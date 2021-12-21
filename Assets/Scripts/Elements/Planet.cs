using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public Transform playerSpawn;

    [SerializeField]
    private Transform turretRoot;
    [SerializeField]
    private Transform powerUpRoot;
    [SerializeField]
    private Transform fuelRoot;
    [SerializeField]
    private Transform trashRoot;

    public List<Transform> turretSpawns { private set; get; }
    public List<Transform> powerUpSpawns { private set; get; }
    public List<Transform> fuelSpawns { private set; get; }
    public List<Transform> trashSpawns { private set; get; }

    [SerializeField] private float distance = 7;

    
    private Gravity grav;



    public void InitSpawns()
    {
        turretSpawns = new List<Transform>();
        powerUpSpawns = new List<Transform>();
        fuelSpawns = new List<Transform>();
        trashSpawns = new List<Transform>();

        AddSpawns(turretRoot, turretSpawns);
        AddSpawns(powerUpRoot, powerUpSpawns);
        AddSpawns(fuelRoot, fuelSpawns);

        AddTrash(trashRoot, trashSpawns);

    }

    private void AddSpawns(Transform root, List<Transform> list)
    {
        for (int i = 0; i < root.childCount; i++)
        {
            list.Add(root.GetChild(i));
        }
    }

    private void AddTrash(Transform root, List<Transform> list)
    {
        for (int i = 0; i < root.childCount; i++)
        {
            if (root.GetChild(i).childCount > 0) AddTrash(root.GetChild(i), list);

            else list.Add(root.GetChild(i));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((grav = other.GetComponent<Gravity>()) != null)
        {
            grav.planet = gameObject;
            grav.MoveToDistance(distance);
        }
    }
}
