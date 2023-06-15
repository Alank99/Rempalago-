using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class teleport : MonoBehaviour
{
     public GameObject point; // El destino del teletransporte
    public GameObject playerObject; // Referencia al GameObject del jugador

    private void OnTriggerEnter2D(Collider2D other)  
    {
            if (other.gameObject == playerObject){
                TeleportPlayer();
            }   
    }

    private void TeleportPlayer()
    {
        playerObject.transform.position = point.transform.position; // Teletransporta al jugador a la posición del destino
        Debug.Log("¡Te has teletransportado!");
    }
}

