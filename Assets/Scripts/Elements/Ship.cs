using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour ,IInteractable
{
    public void Interact(GameObject player)
    {
        GameManager.Instance.CheckEnd();
    }
}
