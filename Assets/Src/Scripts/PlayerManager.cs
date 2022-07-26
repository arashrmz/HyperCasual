using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 1f;

    private void FixedUpdate()
    {
        var grounded = Physics.Raycast(transform.position, -transform.up, groundDistance, groundLayer);
        if (!grounded)
        {
            Debug.Log("You lose");
            GameManager.Instance.OnFallDown();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            CollectKey(other.gameObject);
        }
        else if (other.gameObject.tag == "Door")
        {
            GameManager.Instance.OnEnteredDoor();
        }
    }

    private void CollectKey(GameObject keyObject)
    {
        Destroy(keyObject);
        GameManager.Instance.OnKeyCollected();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * groundDistance);
    }
}
