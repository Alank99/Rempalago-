using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Balero : FatherWeapon
{
    [Header("Referencias a prefabs")]
    [SerializeField] Transform posicionInicial;
    [Tooltip("Animador del balero")]
    public GameObject balero;

    // Update is called once per frame
    void Update()
    {
        if (activa)
            UpdatePosition();
    }

    /// <summary>
    /// Recalcula la posicion del balero cada vez que el jugador se mueve
    /// </summary>
    void UpdatePosition()
    {
        Vector3 position;
        position = posicionInicial.position;

        //No Cambiar de direccion el balero mientras en el estado de animacion te mueves
        if (balero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(balero.transform.position);
            Vector3 dir = Input.mousePosition - pos;

            //Rotar el objeto para que apunte al mouse

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            balero.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
        balero.transform.position = position;

        if (balero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Swing"))
            balero.GetComponent<Animator>().SetBool("Swing?", false);
    }

    /// <summary>
    /// Inicia la animacion del balero
    /// </summary>
    private void OnAttackBalero(InputValue state){
        if (activa)
        {
            balero.GetComponent<Animator>().SetBool("Swing?", true);
        }
    }
}
