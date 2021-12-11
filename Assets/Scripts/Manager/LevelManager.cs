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
        

    public PlayerStats playerStats { get; private set; }

    private Planet actualPlanet;
    private bool[] trashSpawned;
    private int pos;
    int num;
    public void LoadLevel(int level)
    {
        if(!test)
        {
            if (DataManager.Instance.gameFinished)
            {
                actualPlanet = planets[Random.Range(0,level-1)].GetComponent<Planet>();
                print("Loading Random Level");
            }
            else
            {
                actualPlanet = planets[level - 1].GetComponent<Planet>();
            }
            Instantiate(actualPlanet.gameObject);
        }
        else
        {
            actualPlanet = planetInScene;
        }

        actualPlanet.InitSpawns();

        player = Instantiate(playerPrefab, actualPlanet.playerSpawn.position, Quaternion.identity);
        playerStats = player.GetComponent<PlayerStats>();

        SpawnItem(actualPlanet.turretSpawns, turretPrefab);
        SpawnItem(actualPlanet.fuelSpawns, fuelPrefab);
        SpawnItem(actualPlanet.powerUpSpawns, powerUpPrefab);

        SpawnTrash(actualPlanet.trashSpawns, trashPrefab);

    }

    private void SpawnItem(List<Transform> positions, GameObject prefab)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Instantiate(prefab, positions[i].position, Quaternion.identity);
        }
    }

    private void SpawnTrash(List<Transform> positions, GameObject prefab)
    {
        trashSpawned = new bool[positions.Count];
        num = GameManager.Instance.GetExtraTrash();

        for (int i = 0; i < num; i++)
        {
            do
            {
                pos = Random.Range(0, positions.Count);
 
            } while (trashSpawned[pos]);

            trashSpawned[pos] = true;

            Instantiate(prefab, positions[pos].position, Quaternion.identity);
        }
    }
}
