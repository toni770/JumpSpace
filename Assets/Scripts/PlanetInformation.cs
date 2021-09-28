using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInformation : MonoBehaviour
{
    Transform guide;

    void Awake()
    {
        guide = transform.GetChild(0);
        guide.transform.Rotate(Random.Range(0,360), 0, 0);
    }

    public Transform GetGuide()
    {
        return guide;
    }
    

}
