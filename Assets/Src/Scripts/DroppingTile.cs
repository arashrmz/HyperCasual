using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingTile : MonoBehaviour
{
    [SerializeField] private float timeToDrop = 2f;
    [SerializeField] private float force = 1f;

    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Drop()
    {
        StartCoroutine(DropCoroutine());
    }

    private IEnumerator DropCoroutine()
    {
        yield return new WaitForSeconds(timeToDrop);
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.down * force, ForceMode.Impulse);
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

}
