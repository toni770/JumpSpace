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
    private GameObject[] planetPrefabList;
    [SerializeField]
    private GameObject[] planetEndList;

    private GameObject actualPlanet;

    //vars
    GameObject empty;
    float radians, vertical, horizontal;
    int i, rand;
    Vector3 spawnDir, spawnPos;
    GameObject planetObj;
    PlanetInformation planetInfo;

    public void ChangePlanet(GameObject planet) { actualPlanet = planet; }

    public void SpawnPlanets(bool end)
    {
        if(!end) SpawnAroundAlgorithm(Random.Range(minPlanets, maxPlanets + 1), planetPrefabList); //Spawn next planets
        else  SpawnAroundAlgorithm(1, planetEndList); //Spawn the goal
    }

    private void SpawnAroundAlgorithm(int numOfPlanets, GameObject[] planetPrefab) //Spawn gameobject in a circle around a point
    {
        //Creates an empty gameObject to store the planets
        empty = new GameObject();
        empty.name = "PlanetGroup";
        empty.transform.position = actualPlanet.transform.position;

        planetInfo = actualPlanet.GetComponent<PlanetInformation>();

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
            Vector3 spawnPos = actualPlanet.transform.position + spawnDir * (15 * (actualPlanet.transform.localScale.x /planetInfo.GetInitialScale())); // Radius is just the distance away from the point

            /* Now spawn */
            rand = Random.Range(0, planetPrefab.Length);
            planetObj = Instantiate(planetPrefab[rand], spawnPos, Quaternion.identity, empty.transform);
            planetObj.transform.LookAt(actualPlanet.transform);
        }

        empty.transform.rotation = planetInfo.GetGuide().rotation;
    }


}
