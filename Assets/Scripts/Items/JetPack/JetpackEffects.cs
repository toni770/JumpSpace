using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackEffects : MonoBehaviour
{
    [SerializeField] private GameObject[] _fire;
    [SerializeField] private GameObject[] _smoke;
    
    public void ReserveMode(bool reserve)
    {
        for(int i=0;i<_fire.Length;i++) _fire[i].SetActive(!reserve);
        for(int i=0;i<_smoke.Length;i++) _smoke[i].SetActive(reserve);

    }
}
