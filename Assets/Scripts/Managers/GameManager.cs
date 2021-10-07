using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int levels = 5;

    [SerializeField]
    private int minCollectors = 10;

    private int actualLvl = 0;
    private int actualCollector = 0;

    public event Action<GameObject> PlanetChanged = delegate { }; //Subs: GameManager, CameraController, PlayerPhysics

    private PlanetsManager planetsManager;
    private UIManager uiManager;

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
    private void Awake()
    {
        _instance = this;

        PlanetChanged += OnPlanetChanged;

        planetsManager = GetComponent<PlanetsManager>();
        uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        StartGame();
    }

    //PUBLIC FUNCTIONS
    public void ChangePlanet(GameObject planet)
    {
        if (PlanetChanged != null)
            PlanetChanged(planet);
    }

    public void EndAchieved()
    {
        Debug.Log("Game finished");
        Time.timeScale = 0;

        uiManager.ShowEndScreen(actualCollector >= minCollectors);
    }

    public void GetCollector(int quantity)
    {
        actualCollector += quantity;
        uiManager.UpdateCollectorText(actualCollector, minCollectors);
    }


    //PRIVATE FUNCTIONS

    void StartGame()
    {
        actualLvl = 0;
        actualCollector = 0;
        uiManager.UpdateCollectorText(actualCollector, minCollectors);
    }

    private void OnPlanetChanged(GameObject planet)
    {
        actualLvl++;

        if (actualLvl <= levels)
        {
            planetsManager.ChangePlanet(planet);
            planetsManager.SpawnPlanets(actualLvl == levels);
        }
    }
}
