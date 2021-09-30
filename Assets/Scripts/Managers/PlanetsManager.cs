using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsManager : MonoBehaviour
{
    [SerializeField]
    private int minPlanets = 2;
    [SerializeField]
    private int maxPlanets = 4;
    [SerializeField]
    private GameObject planetPrefab;
    [SerializeField]
    private GameObject planetEnd;

    private GameObject actualPlanet;

    //vars
    GameObject empty;
    float radians, vertical, horizontal;
    int i;
    Vector3 spawnDir, spawnPos;
    GameObject planetObj;

    private void Awake()
    {
        GetComponent<GameManager>().PlanetChanged += ChangePlanet;
    }

    private void ChangePlanet(GameObject planet) { actualPlanet = planet; }

    public void SpawnPlanets(bool end)
    {
        if(!end) SpawnAroundAlgorithm(Random.Range(minPlanets, maxPlanets + 1), planetPrefab); //Spawn next planets
        else  SpawnAroundAlgorithm(1, planetEnd); //Spawn the goal
    }

    void SpawnAroundAlgorithm(int numOfPlanets, GameObject planetPrefab) //Spawn gameobject in a circle around a point
    {
        //Creates an empty gameObject to store the planets
        empty = new GameObject();
        empty.name = "PlanetGroup";
        empty.transform.position = actualPlanet.transform.position;

        //For each planet
        for (i = 0; i < numOfPlanets; i++)
        {
            /* Distance around the circle */
            radians = -300 + Mathf.PI / numOfPlanets * i;

            /* Get the vector direction */
            vertical = Mathf.Sin(radians);
            horizontal = Mathf.Cos(radians);

            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            Vector3 spawnPos = actualPlanet.transform.position + spawnDir * 15; // Radius is just the distance away from the point

            /* Now spawn */
            planetObj = Instantiate(planetPrefab, spawnPos, Quaternion.identity, empty.transform);
            planetObj.transform.LookAt(actualPlanet.transform);
        }

        empty.transform.rotation = actualPlanet.GetComponent<PlanetInformation>().GetGuide().rotation;
    }
}
