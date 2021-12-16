using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Transform jetPackPosition;
    [SerializeField] private GameObject[] jetPacksPrefab;

    GameObject actualJetPack;
    private void Start()
    {
        if(DataManager.Instance != null)
        {
            SpawnJetPack(DataManager.Instance.jetpack-1);
        }
    }

    private void SpawnJetPack(int jetNum)
    {
        actualJetPack = Instantiate(jetPacksPrefab[jetNum], parent);
    }

    public void ChangeJetPack(int jetNum)
    {
        if (actualJetPack != null) Destroy(actualJetPack);
        SpawnJetPack(jetNum);
    }
}
