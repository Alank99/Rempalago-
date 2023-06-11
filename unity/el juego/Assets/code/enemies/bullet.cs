using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public Rigidbody2D bulletRB;
    public float speed;

    public float bulletLife;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyAfterTime(bulletLife));
        bulletRB.velocity = new Vector2(speed, bulletRB.velocity.y);
    }
    
    IEnumerator destroyAfterTime(float time){
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}