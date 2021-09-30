using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 9;

    private PlayerPhysics playerPhysics;

    public event Action Move = delegate { }; //Subs: PlayerMovement

    private void Awake()
    {
        playerPhysics = GetComponent<PlayerPhysics>();
        Move += MoveCharacter;
    }

    private void Update()
    {
        if(Move!= null && playerPhysics.isGrounded)
            Move();
    }

    public void GetDirection(Transform guide)
    {
        transform.rotation = Quaternion.LookRotation(guide.transform.forward);
    }

    private void MoveCharacter()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

}
