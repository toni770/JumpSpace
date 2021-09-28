using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float speed = 5;
    public float rotSpeed = 100;

    [Header("Jump")]
    public float jumpForce = 2;
    public float extraGravity = 5;
    public float jumpCD = 0.5f;

    GameObject actualPlanet;

    Rigidbody rb;
    bool isGrounded = true;
    bool jumpCharged = true;
    bool changingPlanet = true;

    //vars
    float jumpCount = 0;


    //UNITY FUNCTIONS
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Gravity();
        if(isGrounded) Move();
        Jump();
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


    //PRIVATE FUNCTIONS
    void Gravity()
    {
        
        if(!isGrounded && !changingPlanet)
        {
            Physics.gravity = (actualPlanet.transform.position - transform.position) * extraGravity;
        }
        else Physics.gravity = actualPlanet.transform.position - transform.position;

        transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;
    }

    void Move()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCharged = false;
        }

        if(!jumpCharged)
        {
            jumpCount += Time.deltaTime;
            if(jumpCount >= jumpCD)
            {
                jumpCount = 0;
                jumpCharged = true;
            }
        }
    }
    
    void GetDirection(Transform guide)
    {
        transform.rotation = Quaternion.LookRotation(guide.transform.forward);
    }

    void ChangePlanet(GameObject planet)
    {
        GameManager.Instance.ChangePlanet(planet);
        GetDirection(planet.GetComponent<PlanetInformation>().GetGuide());
        transform.position = planet.GetComponent<PlanetInformation>().GetGuide().position;

        actualPlanet = planet;
    }

    bool CanJump()
    {
        return isGrounded && jumpCharged;
    }
}
