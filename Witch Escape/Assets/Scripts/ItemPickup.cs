using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.name == "Player")
        {
            Destroy(this.transform.gameObject);
            Debug.Log("Item collected!");
            _other.gameObject.GetComponent<PlayerController>().key = true;
        }
    }
}