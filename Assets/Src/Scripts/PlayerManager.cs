using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int keysToCollect = 5;

    private int _keysCollected = 0;
    private bool _isDoorOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            CollectKey(other.gameObject);
        }
        else if (other.gameObject.tag == "Door")
        {
            if (_isDoorOpen)
            {
                Debug.Log("You win!");
            }
        }
    }

    private void CollectKey(GameObject keyObject)
    {
        Destroy(keyObject);
        _keysCollected++;
        Debug.Log($"{_keysCollected}/{keysToCollect} keys collected");

        if (_keysCollected == keysToCollect)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        _isDoorOpen = true;
        Debug.Log("Door opened");
    }
}
