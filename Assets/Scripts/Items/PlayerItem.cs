using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] private Transform position;
    [SerializeField] private GameObject[] prefabs;

    private GameObject actual;
    private void Spawn(int num, Transform parent)
    {
        if(num >= 0)
            actual = Instantiate(prefabs[num], parent);
    }

    public void Change(int num, Transform parent)
    {
        if (actual != null) Destroy(actual);
        Spawn(num, parent);
    }
}
