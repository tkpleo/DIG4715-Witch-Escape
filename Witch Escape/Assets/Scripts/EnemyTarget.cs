using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTarget : MonoBehaviour
{
    public Transform Target;

    private NavMeshAgent m_Agent;
    private float m_Distance;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Distance = Vector3.Distance(m_Agent.transform.position, Target.position);

        if (m_Distance < 0)
        {
            m_Agent.isStopped = true;
            //player is dead
        }
        else 
        { 
            m_Agent.isStopped = false;
            m_Agent.destination = Target.position;
        }
    }
}
