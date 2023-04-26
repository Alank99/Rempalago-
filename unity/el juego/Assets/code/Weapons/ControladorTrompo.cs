using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControladorTrompo : MonoBehaviour
{
    [Header("Referencias a prefabs")]
    [SerializeField] GameObject trompo;
    [SerializeField] GameObject player;
    [SerializeField] RectTransform chargebar;
    [Tooltip("Tienes la espada?")]
    public bool activa;

    [Header("Caracteristicas del trompo creado")]
    [Tooltip("Maximo tiempo que puede cargar el trompo")]
    [SerializeField] float maxSpeed;
    [Tooltip("Unidades de velocidad que se añaden cada segundo de carga")]
    [SerializeField] float chargepersec;
    [Tooltip("Tiempo entre que el jugador recoge el trompo y lo puede volver a aventar")]
    [SerializeField] float pickupDelay;

    private bool shot = false;
    private float startTime = 0.0f;
    private float startDelay;
    private GameObject nuevoTrompo;

    [Tooltip("Renderer de la barra de carga")]
    [SerializeField] Graphic color;

    /// Update is called once per frame

    void Update()
    {
        if (activa)
        {
            //Dibujar la barra de carga de color rojo si no puedes lanzarlo todavia
            if ((Time.time - startDelay) > pickupDelay)
                color.color = new Color(0,255,0,255);
            else
                color.color = new Color(255,0,0,255);

            if (!shot && startTime != 0.0f)
                printchargeBar(Time.time - startTime);
        }
    }

    /// <summary>
    /// Shoot es llamado cuando quiere usar el trompo el jugador
    /// </summary>
    void shoot(float speed)
    {
            Vector3 shootDirection;
            Vector3 initalPosition;

            shot = true;

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - player.transform.position;
            
            initalPosition = player.transform.position;
            initalPosition.x += Mathf.Sign(shootDirection.x);

            nuevoTrompo = Instantiate(trompo, initalPosition, Quaternion.identity);
            nuevoTrompo.GetComponent<Rigidbody2D>().velocity = shootDirection * speed;
            nuevoTrompo.GetComponent<Rigidbody2D>().velocity += player.GetComponent<Rigidbody2D>().velocity;
            
            //Calcular daño inicial
            nuevoTrompo.GetComponent<Trompo>().setSpinSpeed(speed);
    }

    /// <summary>
    /// Pickup es llamado al querer recoger el trompo
    /// </summary>
    public void pickup()
    {
        if (Mathf.Abs(nuevoTrompo.transform.position.x - player.transform.position.x) < 3 
        &&  Mathf.Abs(nuevoTrompo.transform.position.y - player.transform.position.y) < 3)
        {
            shot = false;
            Destroy(nuevoTrompo);
            startDelay = Time.time;
        }
    }

    /// <summary>
    /// printchargeBar es llamado al tener apretado el boton de disparo para imprimir la barra de disparo
    /// </summary>
    void printchargeBar(float charge)
    {
        //Mover la barra arriba del jugador
        Vector3 new_pos;

        new_pos = Camera.main.WorldToScreenPoint(player.transform.position);
        new_pos.x -= 20;
        new_pos.y += 40;
        chargebar.position = new_pos;

        if (charge > maxSpeed)
            charge = maxSpeed;

        Vector2 length = new Vector2(charge / maxSpeed, 1f);

        chargebar.localScale = length;
    }

    /// <summary>
    /// Regresa cuanto tiempo fue presionado un boton
    /// </summary>
    float heldButton()
    {
        if (Input.GetKeyDown("mouse 0"))
        {           
            startTime = Time.time;
        }
        else if(Input.GetKeyUp("mouse 0"))
        {
            float charge = Time.time - startTime;
            startTime = 0.0f;
            chargebar.localScale = new Vector2(0, 1f);
            return (charge);
        }
        return(0.0f);
    }
    
    /// <summary>
    /// Cada vez que el boton del trompo es presionado o soltado
    /// </summary>
    private void OnAttackTrompo(InputValue state){
        if (activa)
        {
            float held = 0.0f;
            
            if (!shot)
            {
                //Si el boton empieza a ser presionado
                if (state.Get<float>() >= 0.5f)
                {
                    startTime = Time.time;
                    held = 0.0f;
                }
                //Si el boton deja de ser presionado
                else
                {
                    held = Time.time - startTime;
                    startTime = 0.0f;
                    chargebar.localScale = new Vector2(0, 1f);
                }

                if (held > maxSpeed)
                    held = maxSpeed;

                if (held != 0.0f && (Time.time - startDelay) > pickupDelay)
                    shoot(held * chargepersec);
            }
            else
                pickup();
        }
    }
}
