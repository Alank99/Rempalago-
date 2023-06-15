using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class teleport : MonoBehaviour
{
     public Transform teleportDestination; // El destino del teletransporte
    public GameObject playerObject; // Referencia al GameObject del jugador

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F)) // Asegúrate de que el objeto que activa el teletransporte tenga el tag "Player" y se presione la tecla "F"
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        playerObject.transform.position = teleportDestination.position; // Teletransporta al jugador a la posición del destino
        Debug.Log("¡Te has teletransportado!");
    }
}

