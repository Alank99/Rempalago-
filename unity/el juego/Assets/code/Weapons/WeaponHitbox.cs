using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponHitbox : MonoBehaviour
{
    public int damage;
    private int weapon_attack;
    [SerializeField] HealthManager manager;

    public void set_damage(int weapon_damage)
    {
        weapon_attack = weapon_damage;
    }

    /// <summary>
    /// Ataque a un enemigo
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        damage = Mathf.RoundToInt(weapon_attack + manager.player_info.attack);
        if (col.CompareTag("Enemy") || col.CompareTag("charro_hand"))
        {
            try {
            col.GetComponent<genericMonster>().takeDamage(damage);
            }
            catch{
            col.GetComponent<charroHealth>().takeDamage(damage);
            }
        }
    }
}
