using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    public bool ShouldFollow { get; set; }

    void Start()
    {
        ShouldFollow = true;
        offset = transform.position - target.transform.position;
        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnWinner += GameOver;
    }

    private void GameOver()
    {
        ShouldFollow = false;
    }

    void LateUpdate()
    {
        if (ShouldFollow)
            FollowTarget();
    }

    //the camera follows the target smoothly
    void FollowTarget()
    {
        if (target != null)
            transform.position = Vector3.Lerp(transform.position, offset + target.transform.position, Time.deltaTime * speed);
    }
}
