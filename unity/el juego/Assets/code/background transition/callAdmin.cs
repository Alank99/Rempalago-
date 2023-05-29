using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callAdmin : MonoBehaviour
{
    //sabe en que zona esta y llama a la funcion de adminBackground para cambiar el fondo
    public static adminBackground admin;
    [SerializeField] int id;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Transition"){
            admin.changebackground(id);
        }
    }
}
