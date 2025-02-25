using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3[] startPoints;
    public Vector3[] endPoints;
    public float t = 0;
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //setDestination();

       /* startPoints = new Vector3[]
        {
            new Vector3(-13, 1, -51)
        };

        endPoints = new Vector3[]
        {
            new Vector3(-44, 1, -51)
        };*/
    }

    // Update is called once per frame
    void Update()
    {
        
        /*t += Time.deltaTime * speed / Vector3.Distance(startPoints[0], endPoints[0]);
        float easedT = Mathf.SmoothStep(0, 1, t);
        this.transform.position = Vector3.Lerp(startPoints[0], endPoints[0], easedT);

        if (t >= 1f)
        {
            t = 0;
            (startPoints[0], endPoints[0]) = (endPoints[0], startPoints[0]);
        }*/

        /*if(agent.remainingDistance < .05f)
        {
            setDestination();
        }*/

    }

    /*void setDestination()
    {
        var randomPos = new Vector3(Random.Range(0, -50), 0, Random.Range(-43, -55));
        agent.destination = randomPos;
    }*/

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
