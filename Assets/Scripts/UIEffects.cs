using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UIEffects : MonoBehaviour
{
    
    [SerializeField] private bool _upDownMovement = false;
    [SerializeField] private float _upDownDistance = 10;
    [SerializeField] private bool _constantSizable = false;
    private void Start() {
        if(_upDownMovement)
        {
            JuiceManager.Instance.DownUpConstantMovement(transform,_upDownDistance);
        }

        if(_constantSizable)
        {
            JuiceManager.Instance.ConstantSizable(transform);
        }
    }
}
