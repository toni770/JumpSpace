using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        Destroy(gameObject);
    }
}
