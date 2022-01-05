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
        vertical =  joystick.Vertical;//Input.GetAxis("Vertical");
        horizontal = joystick.Horizontal;//Input.GetAxis("Horizontal");
    }

    public void SetJoystick(bl_Joystick _joystick)
    {
        joystick = _joystick;
    }
}
