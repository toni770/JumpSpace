using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public Transform playerSpawn;

    public Transform[] turretSpawns;
    public Transform[] powerUpSpawns;
    public Transform[] fuelSpawns;
    public Transform[] trashSpawns;

    [SerializeField] private float distance = 7;

    
    private Gravity grav;


    private void OnTriggerEnter(Collider other)
    {
        if((grav = other.GetComponent<Gravity>()) != null)
        {
            grav.planet = gameObject;
            grav.MoveToDistance(distance);
        }
    }
}
