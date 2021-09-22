using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    public Transform planet;

    //vars
    Vector3 planetPosition;

    //UNITY FUNCTIONS
    void Update()
    {
        Move();
        Rotate();            
    }

    //PUBLIC FUNCTIONS
    public void ChangePlanet(Transform newPlanet) //Called by: GameManager on player change planet
    {
        planet = newPlanet;
    }

    //PRIVATE FUNCTIONS
    void Move()
    {
        // Define a target position above and behind the target transform
        planetPosition = planet.TransformPoint(new Vector3(0, 0, 3));

        // Smoothly move the camera towards that planet position
        transform.position = Vector3.SmoothDamp(transform.position, planetPosition, ref velocity, smoothTime);
    }

    void Rotate()
    {
        transform.LookAt(player);
    }

}
