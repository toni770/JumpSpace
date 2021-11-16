using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    Gravity grav;
    private void OnTriggerEnter(Collider other)
    {
        if((grav = other.GetComponent<Gravity>()) != null)
        {
            grav.planet = gameObject;
        }
    }
}
