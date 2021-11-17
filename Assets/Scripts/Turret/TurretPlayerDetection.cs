using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlayerDetection : MonoBehaviour
{
    private bool inRange = false;


    private TurretStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.target = other.transform;
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            stateMachine.target = null;
        }
    }

    public bool PlayerInRange()
    {
        return inRange;
    }

}
