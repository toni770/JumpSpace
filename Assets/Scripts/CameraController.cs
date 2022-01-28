using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float transitionSpeed = 2;
    /*public float upDistance = 7.0f;
    public float backDistance = 10.0f;
    public float trackingSpeed = 3.0f;
    public float rotationSpeed = 9.0f;

    private Vector3 v3To;
    private Quaternion qTo;*/

    [SerializeField] private Vector3 pos;
    [SerializeField] private Vector3 rot;


    void Update()
    {
        if(target!=null)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Time.time * transitionSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rot), Time.time * transitionSpeed);
           /* v3To = target.position - target.forward * backDistance + target.up * upDistance;
            transform.position = Vector3.Lerp(transform.position, v3To, trackingSpeed * Time.deltaTime);

            qTo = Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, target.position.z) - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, rotationSpeed * Time.deltaTime);*/
        }

    }

}
