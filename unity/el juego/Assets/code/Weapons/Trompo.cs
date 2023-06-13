using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trompo : MonoBehaviour
{
    [SerializeField] GameObject png;
    [Header("Spin y Daño del trompo")]
    [Tooltip("Cuanto giro pierde el trompo cuando un enemigo entra en contacto con él")]
    [SerializeField] float SpinLoss;
    private float SpinMult = 0.5f;
    [Tooltip("Tiempo que tarda en reducirse el giro del trompo")]
    [SerializeField] float slowdown;

    public ControladorTrompo Player;
    [SerializeField] HealthManager manager;

    public int MaxDamage;

    private float SpinSpeed;
    private float time_since_slow;

    // Start is called before the first frame update
    void Start()
    {
        time_since_slow = Time.unscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime - time_since_slow >= slowdown)
        {
            time_since_slow = Time.unscaledTime;
            SpinSpeed -= 1;
        }
        if (SpinSpeed <= 0)
        {
            this.GetComponent<Rigidbody2D>().freezeRotation = false;
            png.GetComponent<Animator>().SetBool("Spin", false);
            StartCoroutine(reset_stuck(10f));
        }
    }

    IEnumerator reset_stuck(float time) {
        yield return new WaitForSeconds(time);
        Player.pickup();
    }

    /// <summary>
    /// Colisiones con enemigos
    /// </summary>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<genericMonster>().takeDamage((int)(SpinSpeed * MaxDamage));
            SpinSpeed -= SpinLoss;
        }
    }

    /// <summary>
    /// Velocidad inicial del trompo
    /// </summary>
    public void setSpinSpeed(float speed, int max_damage)
    {
        SpinSpeed = speed * SpinMult;
        MaxDamage = Mathf.RoundToInt(max_damage + manager.player_info.attack);
    }
}
