using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMove : MonoBehaviour
{

    //public Transform goal;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        // agent.destination = goal.position;
        agent.destination = GameObject.FindGameObjectWithTag("Destination").transform.position;
    }

    // Update is called once per frame
}
