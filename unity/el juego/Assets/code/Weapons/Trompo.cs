using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trompo : MonoBehaviour
{
    [SerializeField] GameObject trompo;
    [SerializeField] GameObject player;
    [SerializeField] float knockback;
    [SerializeField] float maxSpeed;

    private bool shot = false;
    private float startTime;
    private GameObject nuevoTrompo;

    // Update is called once per frame
    void Update()
    {
        float held = heldButton() * 3;

        //maxima velocidad
        if (held > maxSpeed)
            held = maxSpeed;
    
        if (held != 0.0f && !shot)
        {
            shot = true;
            shoot(held);
        }

        if (shot && Math.Abs(nuevoTrompo.transform.position.x - player.transform.position.x) < 2)
            if (Input.GetKey("mouse 1"))
            {
                shot = false;
                Destroy(nuevoTrompo);
            }


    }

    // Shoot es llamado cuando quiere usar el trompo el jugador
    void shoot(float speed)
    {
        Vector3 shootDirection;
        Vector3 initalPosition;

        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - player.transform.position;
        
        initalPosition = player.transform.position;
        initalPosition.x += Math.Sign(shootDirection.x);

        nuevoTrompo = Instantiate(trompo, initalPosition, Quaternion.identity);
        nuevoTrompo.GetComponent<Rigidbody2D>().velocity = shootDirection * speed;
    }

    //Regresa cuanto tiempo fue presionado un boton
    float heldButton()
    {
        if (Input.GetKeyDown("mouse 0"))
            startTime = Time.time;
        else if(Input.GetKeyUp("mouse 0"))
            return (Time.time - startTime);
        return(0.0f);
    }
}
