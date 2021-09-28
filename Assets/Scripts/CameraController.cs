using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    Transform planet;

    //vars
    Vector3 planetPosition;

    //UNITY FUNCTIONS
    void Update()
    {
        Rotate();
        Move();
    }

    //PUBLIC FUNCTIONS
    public void ChangePlanet(Transform newPlanet) //Called by: GameManager on player change planet
    {
        planet = newPlanet;
    }

    //PRIVATE FUNCTIONS
    void Move()
    {
        if(transform.position != planet.position)
        {
            Vector3 planetNormal = //Vector3.Normalize(planet.position + (planet.gameObject.GetComponent<PlanetInformation>().GetGuide().transform.up));
            // Define a target position above and behind the target transform
            planetPosition = planetNormal*15;//planet.TransformPoint(new Vector3(direccion.x, direccion.y, 3));


            // Smoothly move the camera towards that planet position
            transform.position = Vector3.SmoothDamp(transform.position, planetPosition, ref velocity, smoothTime);
        }
    }

    void Rotate()
    {
        transform.LookAt(planet);
    }

}
