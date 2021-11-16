using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("VARS")]
    public float speed = 10;
    public float maxFuel = 100;
    public float atractorRange = 2;
    [Header("Objects")]

    [SerializeField]
    private SphereCollider atractor;

    private void Awake()
    {
        UpdateAtractorSize();
    }

    private void UpdateAtractorSize()
    {
        atractor.radius = atractorRange;
    }
}
