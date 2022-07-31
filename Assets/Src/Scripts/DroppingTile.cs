using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingTile : MonoBehaviour
{
    [SerializeField] private float timeToDrop = 2f;
    [SerializeField] private float force = 1f;

    private Rigidbody _rigidbody;
    private bool _isDropped = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isDropped)
            transform.Translate(Vector3.down * force * Time.deltaTime);
    }

    public void Drop()
    {
        StartCoroutine(DropCoroutine());
    }

    private IEnumerator DropCoroutine()
    {
        yield return new WaitForSeconds(timeToDrop);
        _isDropped = true;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

}
