using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 1F;
    public float rotSpeed = 1;
    public float distance = 70;
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
            // Define a target position above and behind the target transform
            planetPosition = planet.GetComponent<PlanetInformation>().GetGuide().TransformPoint(new Vector3(0, -distance, 0));

            // Smoothly move the camera towards that planet position
            transform.position = Vector3.SmoothDamp(transform.position, planetPosition, ref velocity, smoothTime);
        }
    }

    void Rotate()
    {
       // transform.LookAt(planet);

        var targetRotation = Quaternion.LookRotation(planet.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

}
