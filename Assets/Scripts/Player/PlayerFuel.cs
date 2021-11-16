using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuel : MonoBehaviour
{


    [SerializeField]
    private float fuelReserveTime = 5;
    [SerializeField]
    private float fuelConsumitionSpeed = 2;

    public bool isReserve { get; private set; } = false;

    private float currentFuel = 0;
    private float reserveCount = 0;

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if(!isReserve)
            GetDamage(Time.deltaTime * fuelConsumitionSpeed);
        else
            if(Time.time >= reserveCount)
                Death();
    }

    private void StartGame()
    {
        RefillFuel();
    }

    public void GetDamage(float damage)
    {
        if (isReserve) Death();
        else
        {
            currentFuel -= damage;
            if (currentFuel <= 0)
            {
                currentFuel = 0;
                GetReserve(true);
            }

            GameManager.Instance.UpdateFuel(currentFuel, playerStats.maxFuel);
        }
    }

    public void RefillFuel()
    {
        currentFuel = playerStats.maxFuel;
        GetReserve(false);
        GameManager.Instance.UpdateFuel(currentFuel, playerStats.maxFuel);
    }

    private void GetReserve(bool reserve)
    {
        isReserve = reserve;
        if(reserve)
        {
            reserveCount = Time.time + fuelReserveTime;
        }
        else
        {
            reserveCount = 0;
        }
    }

    private void Death()
    {
        GameManager.Instance.EndGame(false);
    }
}
