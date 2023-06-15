using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charroHand : charroHealth//MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    private int dado;
    public float d_away;

    void Start()
    {
        salud = 20;
        Tirar_dado();
    }

    void Tirar_dado()
    {
        dado = Random.Range(0, 2);
        Coroutine();
    }

    void Coroutine()
    {

        switch (dado)
        {
            case 0:
                StartCoroutine(Update_patterns(new int[] { 0, 1, 0}));
                break;
            case 1:
                StartCoroutine(Update_patterns(new int[] { 0, 2, 0, 2, 0, 1, 0, 1, 0})); //el chiste de esta corrutina es que la primera parte de la pelea sea más retadora, sim embargo, tiene un patrón muy fijo
                break;
            
        }
    }

    

     // Update is called once per frame
    IEnumerator Update_patterns(int[] patternPoints)
    {
        
        foreach(int i in patternPoints) 
        {
            while (Vector2.Distance(transform.position, patrolPoints[i].position) > d_away)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[i].position, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            
        }

        Tirar_dado();
    }

    
}