using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateMachine : MonoBehaviour
{
	public MonoBehaviour turretOff;
	public MonoBehaviour turretPatrol;
	public MonoBehaviour turretAttack;
	public MonoBehaviour turretStun;

	public MonoBehaviour currentState { get; private set; }

	[HideInInspector]
	public Transform target;

	private void Start()
	{
		currentState = turretPatrol;
		currentState.enabled = true;
	}
	public void NewState(MonoBehaviour state)
	{
		if (currentState != null)
			currentState.enabled = false;

		currentState = state;
		currentState.enabled = true;
	}


}
