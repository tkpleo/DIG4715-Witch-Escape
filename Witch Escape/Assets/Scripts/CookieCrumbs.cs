using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieCrumbs : MonoBehaviour
{
    public GameObject witch;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        witch = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider _other) 
    {

        if (_other.tag == "Player (Detectable)")
        {
            position = _other.transform.position;
            witch.GetComponent<EnemyFieldOfView>().cookieSound = true;
            witch.GetComponent<EnemyFieldOfView>().cookiePosition = position;
            Destroy(gameObject);
        }
        
    }
}
