using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    // Start is called before the first frame update

    private float zDistanceFromPlayer;
    private float yDistanceFromPlayer;
    private float zoom;

    void Start()
    {
        zDistanceFromPlayer = player.position.z - transform.position.z;
        yDistanceFromPlayer = transform.position.y - player.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if(scrollWheel != 0)
        {

            float straightDistanceFromPlayer = Mathf.Sqrt(Mathf.Pow(yDistanceFromPlayer, 2) + Mathf.Pow(zDistanceFromPlayer, 2));

            float newDistanceFromPlayer = straightDistanceFromPlayer + scrollWheel * -10;

            float angle = 35;

            yDistanceFromPlayer = Mathf.Cos(Mathf.Deg2Rad * angle) * newDistanceFromPlayer;

            zDistanceFromPlayer = Mathf.Sin(Mathf.Deg2Rad * angle) * newDistanceFromPlayer;
        }

        transform.position = new Vector3(player.position.x, yDistanceFromPlayer + player.position.y, player.position.z - zDistanceFromPlayer);
    }
}
