using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInformation : MonoBehaviour
{
    [SerializeField]
    private float minSize = 0.1f;
    [SerializeField]
    private float maxSize = 1.2f;
    [SerializeField]
    private Transform guide;
    [SerializeField]
    private SphereCollider col;

    private float initialScale = 1;


    private void Awake()
    {
        ChangeSize();
    }

    public Transform GetGuide()
    {
        return guide;
    }

    public float GetInitialScale()
    {
        return initialScale;
    }

    private void ChangeSize()
    {
        float num = Random.Range(minSize, maxSize);
        initialScale = transform.localScale.x;
        transform.localScale = new Vector3(num, num, num);
        ResizeCollider();
    }

    private void ResizeCollider()
    {
        if (col != null)
            col.radius = col.radius * initialScale / transform.localScale.x;
    }
}
