using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float lifeTime = 3;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float damage = 2;

    private Vector3 dir = Vector3.forward;

    private float lifeCount = 0;

    [SerializeField] private GameObject _effectPrefab;

    private void OnEnable()
    {
        lifeCount = Time.time + lifeTime;
    }

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
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
        ParticlesManager.Instance.SpawnParticle(_effectPrefab,transform.position, transform.rotation);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
