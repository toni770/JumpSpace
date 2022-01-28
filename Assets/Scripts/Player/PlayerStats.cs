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
    private float godCount = 0;

    private void Awake()
    {
        UpdateAtractorSize();
        speed = initSpeed;
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

        switch (currentState)
        {
            case States.normal:
                print("Normal");
                break;
            case States.reserve:
                print("Reserve");
                break;
            case States.god:
                print("God");
                break;
        }
    }

    
    public void Destroy(bool destroy) 
    {
        mesh.SetActive(!destroy);
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
            previousState = currentState;
            currentState = States.god;
            print("ENTER GOD MODE");
            godCount = Time.time + godModeDuration;
        } 
        else
        {
            currentState = previousState;
            print("EXIT GOD MODE");
            godCount = 0;
        }
    }

    public float CurrentSpeed()
    {
        switch (currentState)
        {
            case States.normal:
                return speed;
                break;
            case States.reserve:
                return speed - speed * reserveDiscountSpeed;
                break;
            case States.god:
                return speed + speed * godModeExtraSpeed;
                break;
        }

        return speed;

    }

    public bool IsGodMode()
    {
        return currentState == States.god;
    }
}
