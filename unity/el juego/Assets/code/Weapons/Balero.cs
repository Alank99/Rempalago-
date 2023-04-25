using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Balero : MonoBehaviour
{
[Header("Referencias a prefabs")]
    [Tooltip("Animador del balero")]
    [SerializeField] GameObject balero;
    [Tooltip("Tienes el balero?")]
    public bool activa;

    private float direction = 0.6f;

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
        position = this.transform.position;

        //No Cambiar de direccion el balero mientras en el estado de animacion te mueves
        if (balero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(balero.transform.position);
            Vector3 dir = Input.mousePosition - pos;

            //Rotar el objeto para que apunte al mouse

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            balero.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            if (dir.x > 0)
                direction = 0.6f;
            else if (dir.x < 0)
                direction = -0.6f;
        }
        position.x += direction;
        position.y += 0.5f;
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
