using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject planet;

    [SerializeField]
    private bool staticItem = true;

    private void Awake()
    {
        if (staticItem)
            ApplyGravity();
    }
    private void Update()
    {
        if(!staticItem)
            ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (planet != null)
        {
            Physics.gravity = planet.transform.position - transform.position;

            transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation; //Feet always aim to floor
        }
    }
}
