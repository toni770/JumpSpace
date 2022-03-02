using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffects : MonoBehaviour
{
    
    [SerializeField] private bool _upDownMovement = false;
    [SerializeField] private bool _constantSizable = false;
      [SerializeField] private float _SizableDelay = 0;
    private void Start() {
        if(_upDownMovement)
        {
            JuiceManager.Instance.DownUpConstantMovement(transform);
        }

        if(_constantSizable)
        {
            JuiceManager.Instance.ConstantSizable(transform, _SizableDelay);
        }
    }
}
