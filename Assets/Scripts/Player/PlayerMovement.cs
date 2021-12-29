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
    private float heading;
    private Quaternion desiredRotQ;
    private Vector3 dir;
    private IInteractable interactable;

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
            Rotate();
        }
    }

    private void Move()
    {
       // dir = new Vector3(playerInput.horizontal, 0, playerInput.vertical).normalized * Time.deltaTime * playerStats.CurrentSpeed();
        if (playerInput.horizontal != 0 || playerInput.vertical != 0)  transform.Translate(Vector3.forward * Time.deltaTime * playerStats.CurrentSpeed());
    }

    private void Rotate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -150 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }
        /*
        heading = Mathf.Atan2(playerInput.horizontal, playerInput.vertical);
        if (playerInput.horizontal != 0 || playerInput.vertical != 0)
        {
            desiredRotQ = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rotSmooth);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if((interactable = other.GetComponent<IInteractable>()) != null)
        {
            print("INTERACT");
            interactable.Interact(gameObject);
        }
    }
}
