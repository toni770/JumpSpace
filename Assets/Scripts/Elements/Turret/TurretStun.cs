using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStun : MonoBehaviour
{

    [SerializeField]
    private float stunTime = 5;

    private TurretStateMachine stateMachine;
    private TurretPlayerDetection playerDetection;

    [SerializeField] private ParticleSystem _effect;

    [SerializeField] private AudioConfig _audioStun;
    AudioSource _audioSource;

    float stunCount = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

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
        _audioSource.clip = _audioStun.Clip;
        _audioSource.loop = _audioStun.Loop;
        _audioSource.volume = _audioStun.Volume;
        _audioSource.pitch = _audioStun.Pitch;
        _audioSource.Play();

        _effect.Play();
        stunCount = Time.time + stunTime;
        JuiceManager.Instance.ShakePos(transform,transform.position, 0.2f,0.5f);
    }

    private void OnDisable() {
        _audioSource.Stop();
        _effect.Stop();
    }
}
