using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;


    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    //the camera follows the target smoothly
    void FollowTarget()
    {
        if (target != null)
            transform.position = Vector3.Lerp(transform.position, offset + target.transform.position, Time.deltaTime * speed);
    }
}
