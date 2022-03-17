using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject planet;
    private float planetDist;

    private void Update()
    {
        if (planet!=null) MoveToDistance(planetDist);
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (planet != null)
        {
            Physics.gravity = planet.transform.position - transform.position;

            transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation; //Feet always aim to floor
        }
    }

    public void MoveToDistance(float distance)
    {
        planetDist = distance;

        //Calculate the vector between the object and the player
        Vector3 dir = transform.position - planet.transform.position;
    
                
        transform.position = dir.normalized * distance;
        
    }
}
