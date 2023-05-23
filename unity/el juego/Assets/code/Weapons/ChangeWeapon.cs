using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    [Header("Referencias a clases")]
    [SerializeField] ControladorTrompo Trompo;
    [SerializeField] Balero Balero;
    [SerializeField] Espada Espada;

    [Header("Referencias a imagenes del arma actual")]
    [SerializeField] Image arma; 
    [SerializeField] Sprite ImgTrompo;
    [SerializeField] Sprite ImgBalero;
    [SerializeField] Sprite ImgEspada;


    private int actual = 0;
    private float lastUpdate;

    void start()
    {
        lastUpdate = Time.time;
        arma.sprite = ImgTrompo;
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
                arma.sprite = ImgBalero;
                actual = 1;
            }
            else if(actual == 1){
                Balero.activa = false;
                Espada.activa = true;
                arma.sprite = ImgEspada;
                actual = 2;
            }
            else if(actual == 2){
                Espada.activa = false;
                Trompo.activa = true;
                arma.sprite = ImgTrompo;
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
                arma.sprite = ImgEspada;
                actual = 2;
            }
            else if (actual == 1)
            {
                Balero.activa = false;
                Trompo.activa = true;
                arma.sprite = ImgTrompo;
                actual = 0;
            }
            else if (actual == 2)
            {
                Espada.activa = false;
                Balero.activa = true;
                arma.sprite = ImgBalero;
                actual = 1;
            }
            lastUpdate = Time.time;
        }
    }
}
