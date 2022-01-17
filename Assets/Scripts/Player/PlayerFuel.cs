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
        if(GameManager.Instance.isPlaying)
        {
            if(!playerStats.IsGodMode())
            {
                if (!isReserve)
                    GetDamage(Time.deltaTime * fuelConsumitionSpeed);
                else
                {
                    reserveCount += Time.deltaTime;

                    if(reserveCount >= fuelReserveTime) Death();
                }
            }
        }
    }

    private void StartGame()
    {
        RefillFuel();
    }

    public void GetDamage(float damage)
    {
         if(!playerStats.IsGodMode())
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
    }

    public void RefillFuel()
    {
        currentFuel = playerStats.maxFuel;
        GameManager.Instance.UpdateFuel(currentFuel, playerStats.maxFuel);
        if (isReserve)
            GetReserve(false);
    }

    private void GetReserve(bool reserve)
    {
        isReserve = reserve;
        playerStats.EnterReserve(reserve);
        reserveCount = 0;
    }

    private void Death()
    {
        GameManager.Instance.EndGame(false);
    }
}
