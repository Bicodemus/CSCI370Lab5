using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour {

    public GameObject target;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fromPosition = transform.position;
        Vector3 toPosition = target.transform.position;
        Vector3 direction = toPosition - fromPosition;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit))
		{
            agent.SetDestination(hit.point);
		}

    }
}
