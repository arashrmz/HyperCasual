using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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


}
