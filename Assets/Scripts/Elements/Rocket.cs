using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour, IInteractable
{

    [SerializeField]
    private float lifeTime = 5;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float damage = 10;

    public Transform target;

    private float lifeCount = 0;

    [SerializeField] private GameObject _explosionParticle;

    [SerializeField] private AudioClip _trayectoriaSound;
    [SerializeField] private AudioConfig _explosionSound;
    
    private AudioSource _audioSource;

    private void OnEnable()
    {
        lifeCount = Time.time + lifeTime;
        _audioSource.clip = _trayectoriaSound;
        _audioSource.Play();

    }
    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
        transform.LookAt(target);
        if(Time.time >= lifeCount)
        {
            Death();
        }
    }

    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerFuel>().GetDamage(damage, true);
        Death();
    }

    private void Death()
    {
        SoundManager.Instance.MakeSound(_explosionSound);
        ParticlesManager.Instance.SpawnParticle(_explosionParticle,transform.position, transform.rotation);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
