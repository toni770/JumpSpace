using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            { 
                print("Singleton is null");
            }
            
            return _instance;
        }
    }

    protected virtual void Awake() {
        _instance = this as T;
    }
}

