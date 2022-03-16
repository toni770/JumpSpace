using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    enum item {Hats,Jetpacks}
    [SerializeField] item itemName;
    [SerializeField] private Transform position;
    [SerializeField] private GameObject[] prefabs;

    private GameObject actual;
    private MeshRenderer[] _materials;

    private JetpackEffects _jetpackEffects;
    private void Spawn(int num, Transform parent)
    {
        if(num >= 0)
        {
            actual = Instantiate(prefabs[num], parent);

            if(itemName == item.Jetpacks) 
            {
                _jetpackEffects = actual.GetComponent<JetpackEffects>();
            }

            if(actual.GetComponent<MeshRenderer>() != null)
            {
                _materials = new MeshRenderer[1];
                _materials[0] = actual.GetComponent<MeshRenderer>();
            }
            else _materials = actual.transform.GetComponentsInChildren<MeshRenderer>();
        }
    }

    public void Change(int num, Transform parent)
    {
        if (actual != null) Destroy(actual);
        Spawn(num, parent);
    }

    public void GodMode(bool god)
    {
        if(actual != null)
        {
            foreach(MeshRenderer mat in _materials)
            {
                foreach(Material material in mat.materials)
                {
                    material.SetInt("_Inmune",god?1:0);
                }
            
            }
        }
        
    }

    public void ReserveMode(bool reserve)
    {
        if(itemName == item.Jetpacks)
            _jetpackEffects.ReserveMode(reserve);
    }
}
