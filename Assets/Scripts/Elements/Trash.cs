using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{

    [SerializeField]
    private float atractSpeed = 5;

    private bool atract = false;
    private Transform targetPos;

    private void Update()
    {
        if(atract)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * atractSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Atractor"))
        {
            atract = true;
            targetPos = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Atractor"))
        {
            atract = false;
            targetPos = null;
        }
    }
    public void Interact(GameObject player)
    {
        GameManager.Instance.GetTrash();
        Destroy(gameObject);
    }

    
}
