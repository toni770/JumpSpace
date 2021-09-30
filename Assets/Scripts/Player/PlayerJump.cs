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

    private float nextJumpTime = 0;

    private void Awake()
    {
        GetComponent<PlayerInput>().Jump += Jump;
        rb = GetComponent<Rigidbody>();
        playerPhysics = GetComponent<PlayerPhysics>();
    }

    public bool CanJump()
    {
        return playerPhysics.isGrounded && JumpCharged();
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        playerPhysics.isGrounded = false;
        nextJumpTime = Time.time + jumpCD;
    }

    private bool JumpCharged()
    {
        return Time.time >= nextJumpTime;
    }


}
