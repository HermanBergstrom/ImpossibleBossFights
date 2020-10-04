using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MovingFire : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent agent;
    private readonly float maxX = 108;
    private readonly float minX = 44;
    private readonly float maxZ = 115;
    private readonly float minZ = 86;
    private bool isMoving = false;
    private readonly float timeInterval = 2;
    private float remainingWait;
    private Vector3 targetDestination;
    void Start()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.radius = 0;
        agent.speed = 3;
        remainingWait = timeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && Vector3.Distance(targetDestination, agent.transform.position) < 1)
        {
            isMoving = false;
            remainingWait = timeInterval;
        }

        if (!isMoving && remainingWait > 0)
        {
            remainingWait -= Time.deltaTime;
        }else if (!isMoving && remainingWait <= 0)
        {
            MoveToRandomPosition();
            isMoving = true;
        }
    }

    private void MoveToRandomPosition()
    {
        Vector3 requestedPoint = new Vector3(Random.Range(minX, maxX), 41, Random.Range(minZ, maxZ));
        NavMeshHit hit = new NavMeshHit();
        NavMesh.SamplePosition(requestedPoint, out hit, 10, -1);
        agent.SetDestination(hit.position);
        targetDestination = hit.position;
    }
}
