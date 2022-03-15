using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStun : MonoBehaviour
{

    [SerializeField]
    private float stunTime = 5;

    private TurretStateMachine stateMachine;
    private TurretPlayerDetection playerDetection;

    float stunCount = 0;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
        playerDetection = GetComponent<TurretPlayerDetection>();
    }

    private void Update()
    {
        if(Time.time >= stunCount)
        {
            if(playerDetection.PlayerInRange())
                stateMachine.NewState(stateMachine.turretAttack);
            else
                stateMachine.NewState(stateMachine.turretPatrol);
        }
    }
    private void OnEnable()
    {
        stunCount = Time.time + stunTime;
        JuiceManager.Instance.ShakePos(transform,transform.position, 0.2f,0.5f);
    }
}
