using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponHitbox : MonoBehaviour
{
    public int damage;
    [SerializeField] HealthManager manager;

    public void set_damage(int weapon_damage)
    {
        damage = Mathf.RoundToInt(weapon_damage * manager.player_info.attack);
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
