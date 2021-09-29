using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerJump))]
public class PlayerInput : MonoBehaviour
{
    private PlayerJump playerJump;

    public event Action Jump = delegate{};

    private void Awake()
    {
        playerJump = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerJump.CanJump())
        {
            if(Jump != null)
                Jump();
        }
    }
}
