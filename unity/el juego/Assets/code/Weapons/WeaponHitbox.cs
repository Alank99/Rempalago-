using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponHitbox : MonoBehaviour
{
    public int damage;

    public void set_damage(int weapon_damage)
    {
        damage = weapon_damage;
    }

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
