using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handMovement : MonoBehaviour
{

public Transform[] patrolPoints;
public float moveSpeed;
public int patrolDestination;

    // Update is called once per frame
    void Update()
    {
        if(patrolDestination == 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[4].position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[4].position) < 0.2f)
            {
                patrolDestination = 5;
            }
        }

        if(patrolDestination == 5)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[5].position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[5].position) < 0.2f)
            {
                patrolDestination = 4;
            }
        }
    }
}
