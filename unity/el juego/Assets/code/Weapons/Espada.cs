using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Espada : MonoBehaviour
{
    [Header("Referencias a prefabs")]
    [Tooltip("Animador de la espada")]
    [SerializeField] GameObject espada;
    [Tooltip("Dibujar la espada siempre o no?")]

    private float direction = 0.6f;

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    /// <summary>
    /// Recalcula la posicion de la espada cada vez que el jugador se mueve
    /// </summary>
    void UpdatePosition()
    {
        Vector3 position;
        position = this.transform.position;

        //No Cambiar de direccion la espada mientras en el estado de animacion te mueves
        if (espada.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            if (this.GetComponent<playerController>().movement.x > 0)
                direction = 0.6f;
            else if (this.GetComponent<playerController>().movement.x < 0)
                direction = -0.6f;
        }
        position.x += direction;
        position.y += 0.5f;
        espada.transform.position = position;

        if (espada.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Swing"))
        {
            //color.material.color = new Color(0,255,0,0);
            espada.GetComponent<Animator>().SetBool("Swing?", false);
        }
    }

    /// <summary>
    /// Inicia la animacion de la espada
    /// </summary>
    private void OnAttackSword(InputValue state){
        if (direction > 0)
            espada.GetComponent<Animator>().SetBool("Left?", false);
        else
            espada.GetComponent<Animator>().SetBool("Left?", true);

        //color.material.color = new Color(0,255,0,255);
        espada.GetComponent<Animator>().SetBool("Swing?", true);

    }
}
