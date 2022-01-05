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
                Debug.LogError("Singleton is null");
            }
            
            return _instance;
        }
    }

    protected virtual void Awake() {
        _instance = this as T;
    }
}

public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }

    protected virtual void OnLevelWasLoaded(int level)
    {
       _instance = this as T;
    }
}
