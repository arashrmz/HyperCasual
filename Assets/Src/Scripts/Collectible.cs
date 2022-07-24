using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 6.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
