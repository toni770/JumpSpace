using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float smoothTime = 1F;
    [SerializeField]
    private float rotSpeed = 1;
    [SerializeField]
    private float distance = 70;

    private Vector3 velocity = Vector3.zero;

    private Transform planet;

    //vars
    private Vector3 planetPosition;

    //UNITY FUNCTIONS
    private void Awake()
    {
        GameManager.Instance.PlanetChanged += ChangePlanet;
    }
    private void Update()
    {
        if(planet != null)
        {
            Rotate();
            Move();
        }
    }

    private void ChangePlanet(GameObject newPlanet) 
    {
        planet = newPlanet.transform;
    }

    private void Move()
    {
        if(transform.position != planet.position)
        {
            // Define a target position above and behind the target transform
            planetPosition = planet.GetComponent<PlanetController>().GetGuide().TransformPoint(new Vector3(0, -distance, 0));

            // Smoothly move the camera towards that planet position
            transform.position = Vector3.SmoothDamp(transform.position, planetPosition, ref velocity, smoothTime);
        }
    }

    private void Rotate()
    {

        var targetRotation = Quaternion.LookRotation(planet.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

}
