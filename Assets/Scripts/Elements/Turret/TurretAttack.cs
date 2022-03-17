using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float rotSpeed = 4;
    [SerializeField] private Transform model;

    [SerializeField] private BulletType[] bullets;

    private BulletType actualBullet;

    private TurretStateMachine stateMachine;
    private TurretPlayerDetection playerDetection;
    private float shootCount = 0;

    private Vector3 targetPos;
    private GameObject obj;
 
    private Vector3 _originalSize;

    [SerializeField] private AudioConfig _audioAttack;
    private AudioSource _audioSource;

    private void Awake()
    {
        stateMachine = GetComponent<TurretStateMachine>();
        playerDetection = GetComponent<TurretPlayerDetection>();
        shootCount = 0;

        //actualBullet = bullets[Random.Range(0,bullets.Length)];
        actualBullet = bullets[Random.Range(0,2)];

        _originalSize = transform.localScale;

        _audioSource = GetComponent<AudioSource>();
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
            shootCount = Time.time + actualBullet.shootSpeed;
        }
    }

    public void Rotate()
    {
        var pos = ProjectPointOnPlane(transform.up, transform.position, stateMachine.target.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos, transform.up), Time.deltaTime * rotSpeed);
    }

    private Vector3 ProjectPointOnPlane(Vector3 planeNormal , Vector3 planePoint , Vector3 point)
    {
     planeNormal.Normalize();
     var distance = -Vector3.Dot(planeNormal.normalized, (point - planePoint));
     return point + planeNormal * distance;
    }


    private void Shoot()
    {
        obj = BulletPooling.Instance.GetPooledObj(actualBullet.id);
        obj.transform.position = bulletPosition.position;
        obj.transform.rotation = model.rotation;
        obj.SetActive(true);
        JuiceManager.Instance.ShakeScale(transform, _originalSize, actualBullet.strength,0.5f);
    }

    private void OnEnable()
    {
        _audioSource.clip = _audioAttack.Clip;
        _audioSource.loop = _audioAttack.Loop;
        _audioSource.volume = _audioAttack.Volume;
        _audioSource.pitch = _audioAttack.Pitch;
        _audioSource.Play();

        shootCount = Time.time + actualBullet.firstShootDelay;
    }


}
