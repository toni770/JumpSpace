using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : Singleton<BulletPooling>
{
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private int numObjects =  2;
    private List<GameObject>[] objects;

    private GameObject obj;


    protected override void Awake()
    {
        base.Awake();
        objects = new List<GameObject>[numObjects];
        for(int i=0;i<objects.Length;i++)
        {
            objects[i] = new List<GameObject>();
        }
    }
    public GameObject GetPooledObj(int type)
    {
        for (int i = 0; i < objects[type].Count; i++)
        {
            if(!objects[type][i].activeInHierarchy)
            {
                return objects[type][i];
            }
        }

        return AddItem(type);
    }

    private GameObject AddItem(int type)
    {
        obj = Instantiate(prefab[type]);
        obj.SetActive(false);
        objects[type].Add(obj);

        return obj;
    }
    
}
