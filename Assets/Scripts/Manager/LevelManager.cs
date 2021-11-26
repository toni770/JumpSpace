using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Testing")]
    public Planet planetInScene;
    public bool test = false;

    [Space]
    public GameObject[] planets;

    
    public GameObject player { private set; get; }

    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private GameObject fuelPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private GameObject trashPrefab;
        


    private Planet actualPlanet;
    public void LoadLevel(int level)
    {
        if(!test)
        {
            actualPlanet = planets[level - 1].GetComponent<Planet>();
            Instantiate(actualPlanet.gameObject);
        }
        else
        {
            actualPlanet = planetInScene;
        }

        player = Instantiate(playerPrefab, actualPlanet.playerSpawn.position, Quaternion.identity);

        SpawnItem(actualPlanet.turretSpawns, turretPrefab);
        SpawnItem(actualPlanet.fuelSpawns, fuelPrefab);
        SpawnItem(actualPlanet.powerUpSpawns, powerUpPrefab);
        SpawnItem(actualPlanet.trashSpawns, trashPrefab);

    }

    private void SpawnItem(Transform[] positions, GameObject prefab)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            Instantiate(prefab, positions[i].position, Quaternion.identity);
        }
    }
}
