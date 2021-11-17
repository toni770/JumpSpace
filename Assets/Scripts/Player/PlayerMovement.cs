using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float rotSmooth = 5;

    private PlayerInput playerInput;
    private PlayerStats playerStats;
    private PlayerFuel playerFuel;

    //vars
    float heading;
    Quaternion desiredRotQ;
    private Vector3 dir;
    IInteractable interactable;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
        playerFuel = GetComponent<PlayerFuel>();
    }

    private void Update()
    {
        if(GameManager.Instance.isPlaying)
        {
            Move();
            //Rotate();
        }
    }

    private void Move()
    {
        dir = new Vector3(playerInput.horizontal, 0, playerInput.vertical).normalized * Time.deltaTime * playerStats.speed;
        transform.Translate(dir);
    }

    private void Rotate()
    {
        heading = Mathf.Atan2(playerInput.horizontal, playerInput.vertical);
        if (playerInput.horizontal != 0 || playerInput.vertical != 0)
        {
            //transform.localRotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);
            desiredRotQ = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rotSmooth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((interactable = other.GetComponent<IInteractable>()) != null)
        {
            interactable.Interact(gameObject);
        }
    }
}
