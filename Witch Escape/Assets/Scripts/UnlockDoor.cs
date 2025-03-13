using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player (Detectable)") && collider.gameObject.GetComponent<PlayerController>().key == true)
        {
            SceneManager.LoadScene(2);
        }
    }
}
