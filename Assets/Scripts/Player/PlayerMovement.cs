using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    private Vector3 _originalScale;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
        playerFuel = GetComponent<PlayerFuel>();
        _originalScale = mesh.transform.localScale;

    }

    private void Update()
    {
        if(GameManager.Instance.isPlaying)
        {
            Move();
            Rotate();
        }
        //GetComponent<Gravity>().MoveToDistance(40);
    }

    private void Move()
    {
        Vector3 moveDir = new Vector3(playerInput.horizontal, 0, playerInput.vertical).normalized;
        //Vector3 moveDir = Vector3.forward;
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
        JuiceManager.Instance.ShakeScale(mesh.transform,_originalScale, 0.15f,0.5f);
        print("BOOM");
    }
}
