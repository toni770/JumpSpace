using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private enum States
    {
        normal,
        reserve,
        god
    }

    private States currentState;
    private States previousState;

    [Header("VARS")]
    [SerializeField] private float initSpeed = 10;
    public float maxFuel = 100;
    public float atractorRange = 2;

    [SerializeField] private float godModeDuration = 5;
    [SerializeField] [Range(0, 1)] private float godModeExtraSpeed = 0.5f;
    [SerializeField] [Range(0, 1)] private float reserveDiscountSpeed = 0.5f;

    [Header("Objects")]
    [SerializeField] private SphereCollider atractor;
    [SerializeField] private GameObject mesh;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private float jumpAnimTime = 1f;

    [HideInInspector] public float speed { get; private set; } = 10;

    [SerializeField] private GameObject _skeletton;
    private Material _material;
    private float godCount = 0;

    private PlayerItemsController _playerItemsController;

    private void Awake()
    {
        UpdateAtractorSize();
        speed = initSpeed;
        _material = mesh.GetComponent<SkinnedMeshRenderer>().material;
        _playerItemsController = GetComponent<PlayerItemsController>();
    }
    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if(currentState == States.god && Time.time >= godCount)
        {
            EnterGodMode(false);
        }
    }

    
    public void Destroy(bool destroy) 
    {
        mesh.SetActive(!destroy);
        _skeletton.SetActive(!destroy);
    }

    public void GoShip()
    {
        
        playerAnim.SetTrigger("Ship");
        StartCoroutine(shipAnim());
    }

    IEnumerator shipAnim()
    {
        yield return new WaitForSeconds(jumpAnimTime);
        Destroy(true);
        
    }

    public void InitStats(float _speed, float _range, float _fuel)
    {
        speed = _speed;
        atractorRange = _range;
        maxFuel = _fuel;
    }
    private void StartGame()
    {
        currentState = States.normal;
    }
    private void UpdateAtractorSize()
    {
        atractor.radius = atractorRange;
    }

    public void EnterReserve(bool reserve)
    {
        if(currentState != States.god)
            currentState = reserve ? States.reserve : States.normal;
        
        else
            previousState = reserve ? States.reserve : States.normal;
    }
    public void EnterGodMode(bool god)
    {
        if (god)
        {
            _material.SetInt("_Inmune",1);
            godCount = Time.time + godModeDuration;

            if(currentState != States.god)
            {
                previousState = currentState;
                currentState = States.god;
            }
        } 
        else
        {
            currentState = previousState;
            godCount = 0;
            _material.SetInt("_Inmune",0);
        }

        _playerItemsController.GodMode(god);
    }

    public float CurrentSpeed()
    {
        switch (currentState)
        {
            case States.normal:
                return speed;
            case States.reserve:
                return speed - speed * reserveDiscountSpeed;
            case States.god:
                return speed + speed * godModeExtraSpeed;
        }

        return speed;

    }

    public bool IsGodMode()
    {
        return currentState == States.god;
    }
}
