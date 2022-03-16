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

    private bool _alive = true;

    [SerializeField] private GameObject explosionPrefab;
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

    public void GetDamage(float damage, bool externalDamage = false)
    {
         if(!playerStats.IsGodMode() && _alive)
        {
            if (isReserve) 
                Death();
            else
            {
                if(externalDamage) 
                {
                    JuiceManager.Instance.PlayerDamaged();
                    GameManager.Instance.FuelDamaged();
                }
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
        GameManager.Instance.FuelHealed();
        if(!_alive) _alive = true;
        currentFuel = playerStats.maxFuel;
        GameManager.Instance.UpdateFuel(currentFuel, playerStats.maxFuel);
        if (isReserve)
            GetReserve(false);
    }

    private void GetReserve(bool reserve)
    {
        GameManager.Instance.ReserveMode(reserve);
        isReserve = reserve;
        playerStats.EnterReserve(reserve);
        reserveCount = 0;
    }

    private void Death()
    {
        ParticlesManager.Instance.SpawnParticle(explosionPrefab, transform.position, transform.rotation);

        GameManager.Instance.ReserveMode(false);
        _alive = false;
        JuiceManager.Instance.PlayerDamaged(1);
        GameManager.Instance.EndGame(false);
    }
}
