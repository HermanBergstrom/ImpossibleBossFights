using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
public class CursorMovementScript : MonoBehaviour
{

    private NavMeshAgent agent;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.speed = 10;
        agent.angularSpeed = 600;
        agent.acceleration = 1000;
        GetComponent<PlayerController>().agent = agent;
        cam = Camera.main;

    }



    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
