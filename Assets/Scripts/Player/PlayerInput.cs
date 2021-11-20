using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    [SerializeField]
    private bl_Joystick joystick;

    private void Update()
    {
        GetDirections();
    }

    private void GetDirections()
    {
        vertical =  Input.GetAxis("Vertical");
        horizontal =  Input.GetAxis("Horizontal");
    }
}
