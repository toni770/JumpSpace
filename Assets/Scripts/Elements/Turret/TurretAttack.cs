using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletPosition;

    [SerializeField]
    private float shootSpeed = 1;
    [SerializeField]
    private float rotSpeed = 4;

    private TurretStateMachine stateMachine;
    private TurretPlayerDetection playerDetection;
    private float shootCount = 0;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
        playerDetection = GetComponent<TurretPlayerDetection>();
        shootCount = 0;
    }

    private void Update()
    {
        if(!playerDetection.PlayerInRange())
        {
            stateMachine.NewState(stateMachine.turretPatrol);
            return;
        }
            


        if (stateMachine.target != null && GameManager.Instance.isPlaying)
        {
            Rotate();
            Attack();
        }
            
    }

    public void Attack()
    {
        if (Time.time >= shootCount)
        {
            Shoot();
            shootCount = Time.time + shootSpeed;
        }
    }

    public void Rotate()
    {
        /* var lookPos = new Vector3(stateMachine.target.position.x, 
                                     transform.position.y , 
                                     stateMachine.target.position.z) - transform.position;*/

        var lookPos = stateMachine.target.position - transform.position;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
    }


    private void Shoot()
    {
        Instantiate(bullet, bulletPosition.position, transform.rotation);
    }

    private void OnEnable()
    {
        print("TURRET: ATTACK");
    }


}
