using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Espada : MonoBehaviour
{
    [Header("Referencias a prefabs")]
    [SerializeField] Transform posicionInicial;
    [Tooltip("Animador de la espada")]
    [SerializeField] GameObject espada;
    [Tooltip("Tienes la espada?")]

    public bool activa;

    // Update is called once per frame
    void Update()
    {

        if (activa)
        {
            if (playerController.mousePosVector(posicionInicial.position).x > 0)
                espada.GetComponent<Animator>().SetBool("Left?", false);
            else
                espada.GetComponent<Animator>().SetBool("Left?", true);
            UpdatePosition();
        }
    }

    /// <summary>
    /// Recalcula la posicion de la espada cada vez que el jugador se mueve
    /// </summary>
    void UpdatePosition()
    {
        espada.transform.position = posicionInicial.position;

        if (espada.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Swing"))
            espada.GetComponent<Animator>().SetBool("Swing?", false);
    }

    /// <summary>
    /// Inicia la animacion de la espada
    /// </summary>
    private void OnAttackSword(InputValue state){
        if (activa)
        {
            espada.GetComponent<Animator>().SetBool("Swing?", true);
        }
    }
}
