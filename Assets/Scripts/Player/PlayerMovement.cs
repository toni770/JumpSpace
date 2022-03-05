using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotSmooth = 5;
    [SerializeField] private GameObject mesh;

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
        //if (playerInput.horizontal != 0 || playerInput.vertical != 0)  transform.Translate(Vector3.forward * Time.deltaTime * playerStats.CurrentSpeed());

        Vector3 moveDir = new Vector3(playerInput.horizontal, 0, playerInput.vertical).normalized;
        transform.Translate(moveDir * playerStats.CurrentSpeed() * Time.deltaTime);
    }

    private void Rotate()
    {
        float heading = Mathf.Atan2(playerInput.horizontal, playerInput.vertical);
        if(playerInput.horizontal!=0 || playerInput.vertical!=0)
        {
            //mesh.transform.localRotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);

            var desiredRotQ = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);

            mesh.transform.localRotation = Quaternion.Lerp(mesh.transform.localRotation, desiredRotQ, Time.deltaTime * rotSmooth);
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if((interactable = other.GetComponent<IInteractable>()) != null)
        {
            interactable.Interact(gameObject);
        }
    }

    public void PlayerBlow()
    {
        JuiceManager.Instance.ShakeScale(mesh.transform,0.15f,0.5f);
        print("BOOM");
    }
}
