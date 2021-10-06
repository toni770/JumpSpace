using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int levels = 5;

    private int actualLvl = 0;

    public event Action EndGame = delegate { }; //Subs: GameManager, UIManager
    public event Action<GameObject> PlanetChanged = delegate { }; //Subs: GameManager, CameraController, PlayerPhysics

    private PlanetsManager planetsManager;

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

        EndGame += FinishGame;
        PlanetChanged += OnPlanetChanged;
        planetsManager = GetComponent<PlanetsManager>();
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
        if (EndGame != null)
            EndGame();
    }

    //PRIVATE FUNCTIONS

    void StartGame()
    {
        actualLvl = 0;
    }

    void FinishGame()
    {
        Debug.Log("Game finished");
        Time.timeScale = 0;
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
