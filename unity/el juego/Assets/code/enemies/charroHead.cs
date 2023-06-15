using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charroHead : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float speed;

    public GameObject gameObjectToSetActive;
    public Transform position;

    public void Update() {
        var speedNormalized = Mathf.Abs(player.transform.position.x - transform.position.x) * speed;

        if (player.transform.position.x > transform.position.x) {
            rb.velocity = new Vector2(speedNormalized, 0);
        }
        else {
            rb.velocity = new Vector2(-speedNormalized, 0);
        }
        if (!GameObject.FindWithTag("charro_hand"))
        {
            StartCoroutine(moveWalls(position.position));
            StartCoroutine(DelayDeath(1f));
        }
    }

    IEnumerator moveWalls(Vector3 position)
    {
        while ((gameObjectToSetActive.transform.position - position).magnitude > 0.1){
            gameObjectToSetActive.transform.position = Vector3.MoveTowards(gameObjectToSetActive.transform.position, position, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        gameObjectToSetActive.transform.position = position;
    }

    IEnumerator DelayDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
    
    public void Start() {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
}
