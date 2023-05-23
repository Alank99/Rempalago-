using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    [SerializeField] int damage;

    /// <summary>
    /// Ataque a un enemigo
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            try {
            col.GetComponent<genericMonster>().takeDamage(damage);
            }
            catch{}
        }
    }
}
