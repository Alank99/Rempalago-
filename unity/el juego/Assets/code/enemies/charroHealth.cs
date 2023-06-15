using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class charroHealth : MonoBehaviour
{

    /// <summary>
    /// monster Rigidbody (to add forces)
    /// </summary>
    protected Rigidbody2D rb;

    protected bool alive = true;

    /// <summary>
    /// Define si el monstruo se encuentra activo o no. 
    /// </summary>
    /// <value></value>
    public bool active { get; private set; } = false;

    public int salud { get; set; } //salud del mostro

    public int damage;
    private bool recently_hit;


    [Header("End of charroHealth")]
    public bool invertAnimation = false;
    
    // get rb reference from self on start
    protected void StartMonster() {
        recently_hit = false;
        active = true;
        rb = gameObject.GetComponent<Rigidbody2D>();

        monsterHasActivated();
    }


    /// <summary>
    /// Kills the monster
    /// Does all the things it has to do when it dies
    /// </summary>
    private void killSelf(){
        alive = false;
        var rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = false;
        var amount = 10f;
        rb.AddForce(new Vector2(Random.Range(-1,1) * amount, amount), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-1, 1) * amount);
        StartCoroutine(dieDelay());
    }

    IEnumerator dieDelay() {
        yield return new WaitForSecondsRealtime(1);
        Destroy(this.gameObject);
    }

    public void takeDamage(int damage){
        salud -= damage;
        SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        StartCoroutine(colorChange(sprite, 0.2f));
        if (salud <= 0){
            killSelf();
        }
    }

    IEnumerator colorChange(SpriteRenderer color, float time) {
        yield return new WaitForSeconds(time);
        color.color = new Color(255, 255, 255, 255);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!recently_hit && collision.gameObject.tag == "Player" && alive)
        {
            HealthManager.healthSingleton.receiveDamage(damage);
            recently_hit = true;
            StartCoroutine(waiter(1f));
        }
    }

    IEnumerator waiter(float time) {
        yield return new WaitForSeconds(time);
        recently_hit = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PlayerRadius"){
            StartMonster();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "PlayerRadius"){
            monsterHasDeactivated();
            StopAllCoroutines();
            active = false;
        }
    }

    /// <summary>
    /// gets called whenever the player gets close enough to the monster
    /// </summary>
    public void monsterHasActivated()
    {
        Debug.Log("Charro has activated");
    }
    
    /// <summary>
    /// gets called whenever the monster leaves the player radius
    /// </summary>
    public void monsterHasDeactivated()
    {
        Debug.Log("Charro has deactivated");
    }
}
