using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("VARS")]
    [SerializeField]
    private float initSpeed = 10;
    public float maxFuel = 100;
    public float atractorRange = 2;

    [SerializeField] private float godModeDuration = 5;
    [SerializeField] private float godModeExtraSpeed = 3;
    [SerializeField] private float reserveDiscountSpeed = 5;

    [Header("Objects")]
    [SerializeField] private SphereCollider atractor;

    [HideInInspector]
    public float speed { get; private set; } = 10;


    public bool isGodMode { get; private set; } = false;
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
        if(isGodMode && Time.time >= godCount)
        {
            EnterGodMode(false);
        }
    }

    public void InitStats(float _speed, float _range, float _fuel)
    {
        speed = _speed;
        atractorRange = _range;
        maxFuel = _fuel;
    }
    private void StartGame()
    {
        isGodMode = false;
    }
    private void UpdateAtractorSize()
    {
        atractor.radius = atractorRange;
    }

    public void EnterReserve(bool reserve)
    {
        if (reserve) speed -= reserveDiscountSpeed;
        else
            speed += reserveDiscountSpeed;
    }
    public void EnterGodMode(bool god)
    {
        isGodMode = god;
        if (god)
        {
            print("ENTER GOD MODE");
            godCount = Time.time + godModeDuration;
            speed += godModeExtraSpeed;
        } 
        else
        {
            print("EXIT GOD MODE");
            godCount = 0;
            speed -= godModeExtraSpeed;
        }

    }
}
