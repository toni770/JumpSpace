using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour, IInteractable
{

    [SerializeField] private AudioConfig _audio;
    public void Interact(GameObject player)
    {
        SoundManager.Instance.MakeSound(_audio);
        player.GetComponent<PlayerFuel>().RefillFuel();
        player.GetComponent<PlayerMovement>().PlayerBlow();        
        Destroy(gameObject);
    }
}
