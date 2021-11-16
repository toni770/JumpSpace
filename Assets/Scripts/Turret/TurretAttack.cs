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
    [SerializeField][Range(0,1)]
    private float rotDamp = 1;

    private TurretStateMachine stateMachine;
    private float shootCount = 0;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
        shootCount = 0;
    }

    private void Update()
    {
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
        var lookPos = stateMachine.target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotDamp);
    }


    private void Shoot()
    {
        Instantiate(bullet, bulletPosition.position, transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.NewState(stateMachine.turretPatrol);
            stateMachine.target = null;
        }
    }
}
