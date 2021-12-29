using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turredHead : MonoBehaviour, IInteractable
{

    [SerializeField]
    private TurretStateMachine turret;
    
    public void Interact(GameObject player)
    {
        if(player.GetComponent<PlayerStats>().IsGodMode() && turret.currentState != turret.turretStun)
            turret.NewState(turret.turretStun);
    }
}
