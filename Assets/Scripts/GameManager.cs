using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject actualPlanet;
    public CameraController cam;
    public GameObject planetObject;

    public int minPlanets = 2, maxPlanets = 4;

    List<GameObject> planets;


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
        planets = new List<GameObject>();
    }

    //PUBLIC FUNCTIONS
    public void ChangePlanet(GameObject planet)
    {
        if (planets.Count <= 0) planets.Add(planet);

        actualPlanet = planet;
        cam.ChangePlanet(planet.transform);

        SpawnPlanets(Random.Range(minPlanets,maxPlanets+1));

    }
    //PRIVATE FUNCTIONS

    void SpawnPlanets(int num)
    {
        Vector3 point = actualPlanet.transform.position;
        float baseRadius = 15;
        float radius = 0;
        GameObject center = new GameObject();
        center.transform.position = actualPlanet.transform.position;

        for (int i = 0; i < num; i++)
        {

            /* Distance around the circle */
            float radians = -300+  Mathf.PI  / num * i;

            /* Get the vector direction */
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);

            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

            radius = baseRadius;// (actualPlanet.transform.localScale.x/6.5f) * baseRadius;
            /* Get the spawn position */
            Vector3 spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            GameObject plnt = Instantiate(planetObject, spawnPos, Quaternion.identity,center.transform);
            plnt.transform.LookAt(actualPlanet.transform);

        }

        center.transform.rotation = actualPlanet.GetComponent<PlanetInformation>().GetGuide().rotation;
    }
}
