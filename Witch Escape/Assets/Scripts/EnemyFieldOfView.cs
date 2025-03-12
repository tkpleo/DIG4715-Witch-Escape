using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyFieldOfView : MonoBehaviour
{
    public float radius; // field of view radius around player
    [Range(0, 360)]
    public float angle; // viewing angle of enemy
    private float m_Distance;

    public GameObject playerRef; // object enemy is looking for
    private NavMeshAgent m_Agent; // NavMesh variable for enemy

    public LayerMask targetMask; // layer of what the enemy targets
    public LayerMask obstructionMask; // layer of objects that block the enmey's view

    public bool canSeePlayer; // if the player is in the enemy's field of view
    
    public bool cookieSound;
    public Vector3 cookiePosition;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(.02f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // Function for enemy's feild of view
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0) // checking if the player is in the given range of the enemy
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                m_Distance = Vector3.Distance(m_Agent.transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) // raycast vision of the enmey
                {
                    canSeePlayer = true;
                    cookieSound = false;
                    m_Agent.destination = target.position; // if seen move towards the player
                    //Debug.Log("Player detected");
                }
                else
                {
                    canSeePlayer = false;
                    Debug.Log("Player lost");
                }
            }
            else
                canSeePlayer = false;
        }
        else if (cookieSound) 
        {
            m_Agent.destination = cookiePosition;

            if (Vector3.Distance(m_Agent.transform.position, cookiePosition) < 2)
            { 
                cookieSound = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            cookieSound = false;
        }
    }

    // Function for killing the player
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player is killed");
        }
    }

    // check to see if the player is alive
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player is alive");
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}