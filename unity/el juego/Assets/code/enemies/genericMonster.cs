using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class genericMonster : MonoBehaviour
{
    /// <summary>
    /// the position where the monster will try to go to
    /// </summary>
    protected Vector3 targetPos;

    /// <summary>
    /// monster Rigidbody (to add forces)
    /// </summary>
    protected Rigidbody2D rb;

    [Header("MacMovement")]
    public float maxSpeedX;
    public float Force;
    public float jumpForce;
    public Vector2 waitTime;

    bool alive = true;

    public int health { get; private set; }

    public MonsterTargetingType monsterTargetMethod;

    // get rb reference from self on start
    protected void StartMonster() {
        rb = gameObject.GetComponent<Rigidbody2D>();

        monsterHasActivated();

        switch (monsterTargetMethod)
        {
            case MonsterTargetingType.jumps:
                StartCoroutine("randomJumps");
                break;
            case MonsterTargetingType.walk:
                StartCoroutine("randomWalk");
                break;
            case MonsterTargetingType.fly:
                StartCoroutine("randomFly");
                break;
        }
    }

    IEnumerator randomJumps(){
        while (alive){
            // if (Vector3.Magnitude(transform.position - targetPos) < 3f){
            //     Debug.Log("Changind dir");
            //     currentLock = currentLock == pos1 ? pos2 : pos1;
            // }

            var moveTowards = Vector3.MoveTowards(transform.position, targetPos, maxSpeedX) - transform.position;
            rb.velocity =  new Vector2(moveTowards.x * Force, jumpForce);
            
            yield return new WaitForSeconds(Random.Range(waitTime.x, waitTime.y));
        }
    }

    private void killSelf(){
        alive = false;
        Destroy(transform.parent.gameObject);
    }

    public void takeDamage(int damage){
        health -= damage;

        if (health <= 0){
            killSelf();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player")){
            Debug.Log("Se corre esto");
            giveDamage(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            Debug.Log("Se corre esto");
            giveDamage(other.gameObject);
        }

        if (other.CompareTag("PlayerRadius")){
            StartMonster();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("PlayerRadius")){
            monsterHasDeactivated();
            StopAllCoroutines();
        }
    }

    public abstract void giveDamage(GameObject player);
    public abstract void monsterHasActivated();
    public abstract void monsterHasDeactivated();
}


public enum MonsterTargetingType{
    jumps = 0,
    walk = 1,
    fly = 2
}