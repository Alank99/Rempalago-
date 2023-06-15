using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public Rigidbody2D bulletRB;
    public float speed;

    public float bulletLife;

    public int damage;

    public int time;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = new Vector2(speed, bulletRB.velocity.y);
        Destroy(this.gameObject, time);
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthManager.healthSingleton.receiveDamage(damage);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "charro_hand")
        {
            Destroy(gameObject);
        }
    }
}