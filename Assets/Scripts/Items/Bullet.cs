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

    private void Awake()
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
        player.GetComponent<PlayerFuel>().GetDamage(damage);
        Death();
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
