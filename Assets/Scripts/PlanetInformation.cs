using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInformation : MonoBehaviour
{
    Transform guide;
    public float minSize = 6.5f, maxSize = 9f;
    void Awake()
    {
        guide = transform.GetChild(0);
        //ChangeSize();
    }

    public Transform GetGuide()
    {
        return guide;
    }

    void ChangeSize()
    {
        float num = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(num, num, num);
    }
    

}
