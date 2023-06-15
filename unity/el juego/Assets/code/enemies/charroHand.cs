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
        salud = 2;
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
                StartCoroutine(Update_patterns(new int[] { 0, 1, 0, 2, 0})); //funciona
                break;
            case 1:
                StartCoroutine(Update_patterns(new int[] { 0, 2, 0, 1, 0})); //se traba
                break;
            /*
            case 2:
                StartCoroutine(Update_patterns(new int[] { 0, 2, 3, 1, 0})); //se traba
                break;
            */
        }
    }

    

     // Update is called once per frame
    IEnumerator Update_patterns(int[] patternPoints)
    {
        
        foreach(int i in patternPoints) 
        {
            Debug.Log("Going to " + i);
            while (Vector2.Distance(transform.position, patrolPoints[i].position) > d_away)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[i].position, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            
        }
        
        Tirar_dado();
    }

    
}