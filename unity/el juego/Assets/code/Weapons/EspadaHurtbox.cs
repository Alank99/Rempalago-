using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaHurtbox : MonoBehaviour
{
    [SerializeField] int damage;

    /// <summary>
    /// Ataque a un enemigo
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemigos>().addDamage(damage);
        }
    }
}
