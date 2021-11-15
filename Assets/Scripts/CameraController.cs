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
    private Vector3 offset = new Vector3(0, 24.1f, -9.2f);


    private Vector3 velocity = Vector3.zero;


    //vars
    private Vector3 targetPosition;

    private void Update()
    {
        Follow();
        //Rotate();
    }

    private void Follow()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, smoothTime);
    }

    private void Rotate()
    {
        transform.LookAt(player);
    }


}
