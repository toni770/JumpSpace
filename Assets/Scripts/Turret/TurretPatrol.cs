using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPatrol : MonoBehaviour
{

    private TurretStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.NewState(stateMachine.turretAttack);
            stateMachine.target = other.transform;
        }
    }
}
