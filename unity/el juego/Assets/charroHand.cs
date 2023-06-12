using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charroHand : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    private int dado;

    void Start()
    {
        Tirar_dado();
    }

    void Tirar_dado()
    {
        dado = Random.Range(0, 3); 
        HacerMovimientos();
    }

    void HacerMovimientos()
    {

        switch (dado)
        {
            case 0:
                StartCoroutine(Update_pat1());
                break;
            case 1:
                StartCoroutine(Update_pat2());
                break;
            case 2:
                StartCoroutine(Update_pat3());
                break;
        }
    }

    

     // Update is called once per frame
    IEnumerator Update_pat1()
    {
        // regresar mano al origen
        while (transform.position != patrolPoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // mover mano pos 1
        while (transform.position != patrolPoints[1].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // regresar mano al origen
        while (transform.position != patrolPoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Tirar_dado();
        
    }
    

    IEnumerator Update_pat2()
    {
        // regresar mano al origen
        while (transform.position != patrolPoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // mover mano pos 2
        while (transform.position != patrolPoints[2].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[2].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // regresar mano al origen
        while (transform.position != patrolPoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Tirar_dado();
    }


    IEnumerator Update_pat3()
    {
        // regresar mano al origen
        while (transform.position != patrolPoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // mover mano pos 3
        while (transform.position != patrolPoints[3].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[3].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // mover mano pos 1
        while (transform.position != patrolPoints[1].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // regresar mano al origen
        while (transform.position != patrolPoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Tirar_dado();
    }

}
