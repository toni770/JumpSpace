using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{

    [SerializeField]
    private float atractSpeed = 5;

    private bool atract = false;
    private Transform targetPos;

    [SerializeField] private float _minSecs = 2;
    [SerializeField] private float _maxSecs = 7;

    private Animator _anim;

    private float _spawnTime;
    private float _spawnCount;
    private void Awake() {
        _spawnTime = Random.Range(_minSecs, _maxSecs);
        _spawnCount = Time.time + _spawnTime;
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(atract)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * atractSpeed);
        }
        else
        {
            if(Time.time >_spawnCount)
            {
                _anim.SetTrigger("Jump");
                _spawnCount = Time.time + _spawnTime;
            }
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Atractor"))
        {
            atract = true;
            targetPos = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Atractor"))
        {
            atract = false;
            targetPos = null;
        }
    }
    public void Interact(GameObject player)
    {
        GameManager.Instance.GetTrash();
        player.GetComponent<PlayerMovement>().PlayerBlow();
        Destroy(gameObject);
    }
}