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

    private void OnEnable()
    {
        lifeCount = Time.time + lifeTime;
    }
    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
