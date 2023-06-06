using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public Rigidbody2D bulletRB;
    public float speed;

    public float bulletLife;
    public float bulletSelfDestroy;


    // Start is called before the first frame update
    void Start()
    {
        bulletSelfDestroy = bulletLife;
    }

    // Update is called once per frame
    void Update()
    {
        bulletSelfDestroy -= Time.deltaTime;
        if (bulletSelfDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        bulletRB.velocity = new Vector2(speed, bulletRB.velocity.y);
    }
}
