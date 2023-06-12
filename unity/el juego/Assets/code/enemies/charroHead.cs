using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charroHead : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float speed;

    public void Update() {
        var speedNormalized = Mathf.Abs(player.transform.position.x - transform.position.x) * speed;

        if (player.transform.position.x > transform.position.x) {
            rb.velocity = new Vector2(speedNormalized, 0);
        }
        else {
            rb.velocity = new Vector2(-speedNormalized, 0);
        }
    }
    
    public void Start() {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
}
