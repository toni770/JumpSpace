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
    bool changingPlanet = false;

    //vars
    float jumpCount = 0;
    float h, v;
    Vector3 movement, vel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
        if (changingPlanet) GameManager.Instance.ChangePlanet(collision.gameObject);
        isGrounded = true;
        changingPlanet = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
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
        if (Input.GetKey(KeyCode.UpArrow)) { transform.Translate(new Vector3(0, 0, speed * Time.deltaTime)); }
        if (Input.GetKey(KeyCode.DownArrow)) { transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime)); }
        if (Input.GetKey(KeyCode.LeftArrow)) { transform.Rotate(0, -rotSpeed * Time.deltaTime,0); }
        if (Input.GetKey(KeyCode.RightArrow)) { transform.Rotate(0, rotSpeed * Time.deltaTime, 0); }
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

    bool CanJump()
    {
        return isGrounded && jumpCharged;
    }
}
