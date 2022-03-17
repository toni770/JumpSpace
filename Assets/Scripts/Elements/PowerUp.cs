using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioConfig _audio;
    public void Interact(GameObject player)
    {
        SoundManager.Instance.MakeSound(_audio);
        player.GetComponent<PlayerStats>().EnterGodMode(true);
        player.GetComponent<PlayerMovement>().PlayerBlow();
        Destroy(gameObject);
    }
}
