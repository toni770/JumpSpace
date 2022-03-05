using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerStats>().EnterGodMode(true);
        player.GetComponent<PlayerMovement>().PlayerBlow();
        Destroy(gameObject);
    }
}
