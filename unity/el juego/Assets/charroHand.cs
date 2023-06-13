using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charroHand : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    private int dado;
    public float d_away;

    void Start()
    {
        Tirar_dado();
    }

    void Tirar_dado()
    {
        dado = Random.Range(0, 3); 
        dado = 1;
        Coroutine();
    }

    void Coroutine()
    {

        switch (dado)
        {
            case 0:
                StartCoroutine(Update_pat1(new int[] { 0, 1, 0}));
                break;
            case 1:
                StartCoroutine(Update_pat1(new int[] { 0, 2, 0}));
                break;
            case 2:
                StartCoroutine(Update_pat1(new int[] { 0, 3, 1, 0}));
                break;
        }
    }

    

     // Update is called once per frame
    IEnumerator Update_pat1(int[] patternPoints)
    {
        // regresar mano al origen
        foreach(int i in patternPoints) 
        {
            Debug.Log("Going to " + i);
            while (Vector2.Distance(transform.position, patrolPoints[i].position) > d_away)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[i].position, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        Debug.Log("DONE");
        
        Tirar_dado();
        
    }
}