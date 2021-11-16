using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerFuel>().RefillFuel();
        Destroy(gameObject);
    }
}
