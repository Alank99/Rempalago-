using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class ControladorTrompo : FatherWeapon
{
    [Header("Referencias a prefabs")]
    [SerializeField] GameObject trompo;
    [SerializeField] GameObject player;
    [SerializeField] RectTransform chargebar;
    [SerializeField] Transform posicionInicial;

    [Header("Caracteristicas del trompo creado")]
    [Tooltip("Maximo tiempo que puede cargar el trompo")]
    [SerializeField] float maxSpeed;
    [Tooltip("Unidades de velocidad que se añaden cada segundo de carga")]
    [SerializeField] float chargepersec;
    [Tooltip("Tiempo entre que el jugador recoge el trompo y lo puede volver a aventar")]
    [SerializeField] float pickupDelay;
    private int max_damage;

    private bool shot = false;
    private float startTime = 0.0f;
    private float startDelay;
    private bool pickupable;
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

    public void set_damage(int weapon_damage)
    {
        max_damage = weapon_damage;
    }

    /// <summary>
    /// Shoot es llamado cuando quiere usar el trompo el jugador
    /// </summary>
    void shoot(float speed)
    {
            Vector2 shootDirection = playerController.mousePosVector(posicionInicial.position);
            Vector3 initalPosition;

            shot = true;
            pickupable = false;
            
            initalPosition = posicionInicial.position;
            initalPosition.x += Mathf.Sign(shootDirection.x);

            nuevoTrompo = Instantiate(trompo, initalPosition, Quaternion.identity);
            nuevoTrompo.GetComponent<Rigidbody2D>().velocity = shootDirection * speed * 2f;
            nuevoTrompo.GetComponent<Rigidbody2D>().velocity += player.GetComponent<Rigidbody2D>().velocity;
            
            //Calcular daño inicial
            nuevoTrompo.GetComponent<Trompo>().setSpinSpeed(speed, max_damage);
            StartCoroutine(prevent_pickup(0.5f));
    }

    IEnumerator prevent_pickup(float time)
    {
        yield return new WaitForSeconds(time);
        pickupable = true;
    }

    /// <summary>
    /// Pickup es llamado al querer recoger el trompo
    /// </summary>
    public void pickup()
    {
        if (pickupable)
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
        Vector3 velocity = Vector3.zero;

        new_pos = Camera.main.WorldToScreenPoint(player.transform.position);
        new_pos.x -= 20;
        new_pos.y += 40;
        
        //El objeto se teletransporta arriba del jugador
        //chargebar.position = new_pos;

        //El objeto se mueve cerca del jugador
        //chargebar.position = Vector3.SmoothDamp(chargebar.position, new_pos, ref velocity, 0.1f);

        //El objeto se mueve de forma mas limpia al jugador
        chargebar.position = Vector3.SmoothDamp(chargebar.position, new_pos, ref velocity, 0.02f);

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
