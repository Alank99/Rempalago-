using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeWeapon : MonoBehaviour
{
    [Header("Referencias a clases")]
    [SerializeField] ControladorTrompo Trompo;
    [SerializeField] Balero Balero;
    [SerializeField] Espada Espada;

    private int actual = 0;
    private float lastUpdate;

    void start()
    {
        lastUpdate = Time.time;
    }

    /// <summary>
    /// Cambia el arma actual del jugador
    /// </summary>
    private void OnCambiarArma(InputValue state){
        //Si tienes menos de un segundo que cambiaste de arma, no cambies
        if (Time.time - lastUpdate < 1.0f) return;

        Vector2 direction = state.Get<Vector2>();

        if (direction.y > 0)
        {
            if(actual == 0){
                Trompo.activa = false;
                Balero.activa = true;
                actual = 1;
            }
            else if(actual == 1){
                Balero.activa = false;
                Espada.activa = true;
                actual = 2;
            }
            else if(actual == 2){
                Espada.activa = false;
                Trompo.activa = true;
                actual = 0;
            }
            lastUpdate = Time.time;
        }
        else if (direction.y < 0)
        {
            if (actual == 0)
            {
                Trompo.activa = false;
                Espada.activa = true;
                actual = 2;
            }
            else if (actual == 1)
            {
                Balero.activa = false;
                Trompo.activa = true;
                actual = 0;
            }
            else if (actual == 2)
            {
                Espada.activa = false;
                Balero.activa = true;
                actual = 1;
            }
            lastUpdate = Time.time;
        }
    }
}
