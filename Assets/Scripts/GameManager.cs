using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject actualPlanet;
    public CameraController cam;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is NULL");

            return _instance;
        }
    }

    //UNITY FUNCTIONS
    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        
    }
    //PUBLIC FUNCTIONS
    public void ChangePlanet(GameObject planet)
    {
        actualPlanet = planet;
        cam.ChangePlanet(planet.transform);

    }
    //PRIVATE FUNCTIONS
}
