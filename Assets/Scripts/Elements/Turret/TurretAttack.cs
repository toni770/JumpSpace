using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletPosition;

    [SerializeField] private float shootSpeed = 1;
    [SerializeField] private float rotSpeed = 4;

    [SerializeField] private Transform model;

    private TurretStateMachine stateMachine;
    private TurretPlayerDetection playerDetection;
    private float shootCount = 0;

    private Vector3 rotationMask;
    private GameObject obj;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
        playerDetection = GetComponent<TurretPlayerDetection>();
        shootCount = 0;
        rotationMask = new Vector3(0, 1, 0);
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
        var rotation = Quaternion.LookRotation(stateMachine.target.position - transform.position);

        // Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(model.rotation.eulerAngles.x, rotation.eulerAngles.y, model.rotation.eulerAngles.z);
        // var rot = Quaternion.Euler(Vector3.Scale(rotation, rotationMask));
        model.rotation = LookAtRotationOnly_Y;
       // model.localRotation = Quaternion.Slerp(model.localRotation, rot, rotSpeed * Time.deltaTime);
    }


    private void Shoot()
    {
        obj = BulletPooling.Instance.GetPooledObj();
        obj.transform.position = bulletPosition.position;
        obj.transform.rotation = model.rotation;
        obj.SetActive(true);
    }

    private void OnEnable()
    {
        print("TURRET: ATTACK");
    }


}
