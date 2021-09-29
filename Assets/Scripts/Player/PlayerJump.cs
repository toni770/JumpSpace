using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 20;
    [SerializeField]
    private float jumpCD = 0.7f;

    private PlayerPhysics playerPhysics;
    private Rigidbody rb;

    private bool jumpCharged = true;

    private float jumpCount = 0;

    private void Awake()
    {
        GetComponent<PlayerInput>().Jump += Jump;
        rb = GetComponent<Rigidbody>();
        playerPhysics = GetComponent<PlayerPhysics>();
    }

    private void Update()
    {
        if (!jumpCharged)
        {
            jumpCount += Time.deltaTime;
            if (jumpCount >= jumpCD)
            {
                jumpCount = 0;
                jumpCharged = true;
            }
        }
    }

    public bool CanJump()
    {
        return playerPhysics.isGrounded && jumpCharged;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        playerPhysics.isGrounded = false;
        jumpCharged = false;
    }


}
