using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPatrol : MonoBehaviour
{

    private TurretStateMachine stateMachine;
    private TurretPlayerDetection playerDetection;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
        playerDetection = GetComponent<TurretPlayerDetection>();
    }

    private void Update()
    {
        if(playerDetection.PlayerInRange())
        {
            stateMachine.NewState(stateMachine.turretAttack);
        }
    }

}
