using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Espada : MonoBehaviour
{
    [Header("Referencias a prefabs")]
    [Tooltip("Animador de la espada")]
    [SerializeField] GameObject espada;
    [Tooltip("Tienes la espada?")]
    public bool activa;

    private float direction = 0.6f;

    // Update is called once per frame
    void Update()
    {
        if (activa)
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
            espada.GetComponent<Animator>().SetBool("Swing?", false);
    }

    /// <summary>
    /// Inicia la animacion de la espada
    /// </summary>
    private void OnAttackSword(InputValue state){
        if (activa)
        {
            if (direction > 0)
                espada.GetComponent<Animator>().SetBool("Left?", false);
            else
                espada.GetComponent<Animator>().SetBool("Left?", true);

            espada.GetComponent<Animator>().SetBool("Swing?", true);
        }
    }
}
