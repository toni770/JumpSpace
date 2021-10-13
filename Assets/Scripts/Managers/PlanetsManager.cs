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
    private float radius = 15;

    [SerializeField]
    private int minCollectors = 2;
    [SerializeField]
    private int maxCollectors = 4;

    [SerializeField]
    private GameObject collectorPrefab;
    [SerializeField]
    private GameObject[] planetPrefabList;
    [SerializeField]
    private GameObject[] planetEndList;


    private List<GameObject> planetList;
    private GameObject actualPlanet;

    //vars
    private GameObject emptyGameObject;
    private GameObject groupOfItems, groupOfPlanets;
    private float vertical, horizontal;
    private int i, j, rand;
    private Vector3 spawnDir, spawnPos;
    private GameObject planetObj, itemObj;
    private PlanetController planetInfo;

    private void Awake()
    {
        planetList = new List<GameObject>();
    }
    public void ChangePlanet(GameObject planet) 
    {
        for (int i = 0; i < planetList.Count; i++)
        {
            if (planetList[i] != planet)
                planetList[i].GetComponent<PlanetController>().Destroy();
        }

        actualPlanet = planet;
        planetList.Add(planet);
    }

    public void SpawnPlanets(bool end)
    {
        if (!end) SpawnAroundAlgorithm(Random.Range(minPlanets, maxPlanets + 1), planetPrefabList); //Spawn next planets
        else SpawnAroundAlgorithm(1, planetEndList); //Spawn the goal
    }

    private void SpawnAroundAlgorithm(int numOfPlanets, GameObject[] planetPrefab) //Spawn gameobject in a circle around a point
    {
        //Creates an empty gameObject to store the planets
        groupOfPlanets = CreateEmptyGameObject("PlanetGroup", actualPlanet.transform.position);

        planetInfo = actualPlanet.GetComponent<PlanetController>();

        //For each planet
        for (i = 0; i < numOfPlanets; i++)
        {
            spawnDir = GetSpawnDir(-300 + Mathf.PI / numOfPlanets * i);

            /* Get the spawn position */
            spawnPos = actualPlanet.transform.position + spawnDir * (radius * (actualPlanet.transform.localScale.x / planetInfo.GetInitialScale())); // Radius is just the distance away from the point

            /* Now spawn */
            rand = Random.Range(0, planetPrefab.Length);
            planetObj = Instantiate(planetPrefab[rand], spawnPos, Quaternion.identity, groupOfPlanets.transform);
            planetList.Add(planetObj);

            if (numOfPlanets > 1 && Random.Range(0, 2) == 0)
                SpawnItems(Random.Range(minCollectors, maxCollectors+1), collectorPrefab, planetObj);

            planetObj.transform.LookAt(actualPlanet.transform);
        }

        groupOfPlanets.transform.rotation = planetInfo.GetGuide().rotation;
    }

    private void SpawnItems(int numOfItems, GameObject item, GameObject planet)
    {
        //Creates an empty gameObject to store the items
        groupOfItems = CreateEmptyGameObject("ItemGroup", planet.transform.position);
        groupOfItems.transform.parent = planet.transform;

        //For each item
        for (j = 0; j < numOfItems; j++)
        {
            spawnDir = GetSpawnDir(2 * Mathf.PI/numOfItems *j);

            /* Get the spawn position */
            spawnPos = planet.transform.position + spawnDir * 5; // Radius is just the distance away from the point

            /* Now spawn */
            itemObj = Instantiate(item, spawnPos, Quaternion.identity, groupOfItems.transform);
            itemObj.transform.LookAt(planet.transform);
        }
        groupOfItems.transform.rotation = planet.GetComponent<PlanetController>().GetGuide().rotation;
    }

    private GameObject CreateEmptyGameObject(string name, Vector3 position)
    {
        emptyGameObject = new GameObject();
        emptyGameObject.name = name;
        emptyGameObject.transform.position = position;
        return emptyGameObject;
    }

    private Vector3 GetSpawnDir(float radians)
    {
        /* Get the vector direction */
        vertical = Mathf.Sin(radians);
        horizontal = Mathf.Cos(radians);

        return new Vector3(horizontal, 0, vertical);
    }


}
