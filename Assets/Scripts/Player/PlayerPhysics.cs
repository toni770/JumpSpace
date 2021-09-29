using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{

    [HideInInspector]
    public bool isGrounded = true;

    [SerializeField]
    private float extraGravity = 15;

    private PlayerMovement playerMovement;

    private bool changingPlanet = true;
    private GameObject actualPlanet;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Gravity();
    }

    private void Gravity()
    {
        if (!isGrounded && !changingPlanet)
        {
            Physics.gravity = (actualPlanet.transform.position - transform.position) * extraGravity;
        }
        else Physics.gravity = actualPlanet.transform.position - transform.position;

        transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;
    }
    private void ChangePlanet(GameObject planet)
    {
        GameManager.Instance.ChangePlanet(planet);
        playerMovement.GetDirection(planet.GetComponent<PlanetInformation>().GetGuide());
        transform.position = planet.GetComponent<PlanetInformation>().GetGuide().position;

        actualPlanet = planet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            actualPlanet = other.gameObject;
            changingPlanet = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (changingPlanet)
        {
            ChangePlanet(collision.gameObject);
        }
        isGrounded = true;
        changingPlanet = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
