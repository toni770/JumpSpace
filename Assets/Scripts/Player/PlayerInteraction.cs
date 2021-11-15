using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float maxFuel = 100;
    [SerializeField]
    private float fuelReserveTime = 5;
    [SerializeField]
    private float fuelConsumitionSpeed = 2;

    private float currentFuel = 0;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        GetDamage(Time.deltaTime * fuelConsumitionSpeed);
    }

    private void StartGame()
    {
        currentFuel = maxFuel;
    }

    public void GetDamage(float damage)
    {
        currentFuel -= damage;
        if(currentFuel <= 0)
        {
            currentFuel = 0;
            Death();
        }
    }

    private void Death()
    {

    }

}
