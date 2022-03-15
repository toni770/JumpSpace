using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _distance = 10;

    [SerializeField] private MeshRenderer _rend;
    private void Update() {
        if(_target != null) 
        {
            transform.LookAt(_target, Vector3.up);
            
            _rend.enabled = Vector3.Distance(transform.position,_target.position) > _distance;
        }
    }

    public void AssignTarget(Transform target)
    {
        _target = target;
    }
}
