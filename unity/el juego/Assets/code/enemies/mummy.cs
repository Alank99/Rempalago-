using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    [SerializeField] int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Mandarle daño al objeto
    public void addDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(this.gameObject);
        Debug.Log("Daño: " + damage);
    }
}
