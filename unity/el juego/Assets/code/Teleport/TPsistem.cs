using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {
    //La posición a la que se teleportará el jugador
    public Transform teleportPosition;

    //El objeto al que se teleportará
    public GameObject targetObject;

    //La función que se ejecutará cuando el botón se presione
    public void TeleportToTarget() {
        //Obtener el objeto jugador
        GameObject player = GameObject.FindWithTag("Player");

        //Si el jugador existe y el objeto destino existe
        if (player != null && targetObject != null) {
            //Teleportar al jugador al objeto destino
            player.transform.position = targetObject.transform.position;
        }
    }

    //La función que se ejecutará cuando se seleccione un objeto destino
    public void SetTargetObject(GameObject newTarget) {
        //Asignar el objeto destino seleccionado a la variable targetObject
        targetObject = newTarget;
    }
}