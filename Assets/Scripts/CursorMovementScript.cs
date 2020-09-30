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

    public void SetMaxSpeed(int speed)
    {
        agent.speed = speed;
    }

    public float GetCurrentSpeed()
    {
        return agent.velocity.magnitude;
    }

    public void SetStopped(bool stopped)
    {
        agent.isStopped = stopped;
    }
}
