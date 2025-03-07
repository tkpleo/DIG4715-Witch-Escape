using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Vector3[] startPoints;
    public Vector3[] endPoints;
    public float t = 0;
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        startPoints = new Vector3[]
        {
            new Vector3(-13, 1, -51)
        };

        endPoints = new Vector3[]
        {
            new Vector3(-44, 1, -51)
        };
    }

    // Update is called once per frame
    void Update()
    {
        
        t += Time.deltaTime * speed / Vector3.Distance(startPoints[0], endPoints[0]);
        float easedT = Mathf.SmoothStep(0, 1, t);
        this.transform.position = Vector3.Lerp(startPoints[0], endPoints[0], easedT);

        if (t >= 1f)
        {
            t = 0;
            (startPoints[0], endPoints[0]) = (endPoints[0], startPoints[0]);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
        }   
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }


}
