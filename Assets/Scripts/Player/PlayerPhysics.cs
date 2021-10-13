using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{

    [HideInInspector]
    public bool isGrounded = true;

    [SerializeField]
    private float extraGravity = 15; //Increase gravity for a faster falling

    [SerializeField]
    private GameObject firstPlanet; //Increase gravity for a faster falling

    private PlayerMovement playerMovement;

    private bool changingPlanet = true;
    private GameObject actualPlanet;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        GameManager.Instance.PlanetChanged += SetNewPlanet;
    }
    private void Start()
    {
        SetNewPlanet(firstPlanet);
    }
    private void Update()
    {
        Gravity();
    }

    private void Gravity()
    {
        if (actualPlanet != null)
        {
            Physics.gravity = actualPlanet.transform.position - transform.position;

            if (!isGrounded && !changingPlanet) Physics.gravity *= extraGravity; //Extra gravity on normal jump

            transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation; //Feet always aim to floor
        }
    }
    private void ChangePlanet(GameObject planet)
    {
        GameManager.Instance.ChangePlanet(planet);
    }

    private void SetNewPlanet(GameObject planet) //Set starting position and rotation on new planet
    {
        playerMovement.GetDirection(planet.GetComponent<PlanetController>().GetGuide());
        transform.position = planet.GetComponent<PlanetController>().GetGuide().position;
        actualPlanet = planet;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Planet")) //On enter planet's atmosphere
        {
            actualPlanet = other.gameObject;
            changingPlanet = true;
        }
        else if(other.CompareTag("End"))
        {
            GameManager.Instance.EndAchieved();
        }
        else if(other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.GetCollector(1);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Planet"))
        {
            if (changingPlanet) //New planet achieved
            {
                ChangePlanet(collision.gameObject);
            }
            isGrounded = true;
            changingPlanet = false;
        }
    }
}
