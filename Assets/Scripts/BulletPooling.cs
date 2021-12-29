using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private List<GameObject> objects = new List<GameObject>();

    private GameObject obj;

    private static BulletPooling _instance;

    public static BulletPooling Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Bullet Pooling is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public GameObject GetPooledObj()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if(!objects[i].activeInHierarchy)
            {
                return objects[i];
            }
        }

        return AddItem();
    }

    private GameObject AddItem()
    {
        obj = Instantiate(prefab);
        obj.SetActive(false);
        objects.Add(obj);

        return obj;
    }
    
}
