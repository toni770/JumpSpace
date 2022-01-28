using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    [SerializeField] private Transform cam;
    
    
    private void Update() {
        transform.LookAt(cam, Vector2.up);
    }
}
