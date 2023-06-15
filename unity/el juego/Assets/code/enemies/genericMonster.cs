using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    protected bool alive = true;
    /// <summary>
    /// Define si el mounstro se encuentra activo o no. 
    /// </summary>
    /// <value></value>
    public bool active { get; private set; } = false;

    public int salud { get; set; } //salud del mostro

    public MonsterTargetingType monsterTargetMethod;

    public int damage;
    private bool recently_hit;

    [Header("Loot references")]
    public GameObject lootPrefab;
    public GameObject coinPrefab;
    public Image equippedWeapon;
    public HealthManager manager;
    public int id;

    [Header("End of genericMonster")]
    public bool invertAnimation = false;
    
    // get rb reference from self on start
    protected void StartMonster() {
        recently_hit = false;
        active = true;
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
            //case MonsterTargetingType.fly:
              //  StartCoroutine("randomFly");
              //  break;
        }
    }

    /// <summary>
    /// Make sure the monster is looking the right way when calling this frame
    /// </summary>
    protected void lookCorrectWay(){
        try{
        if (targetPos.x > transform.position.x){
            transform.GetChild(0).localScale = new Vector3(invertAnimation? 1 : -1,1,1);
        } else {
            transform.GetChild(0).localScale = new Vector3(invertAnimation? -1 : 1,1,1);
        }
        } catch{}
    }


    IEnumerator randomJumps(){
        while (alive){
            updateWalkMovement();
            var moveTowards = Vector3.MoveTowards(transform.position, targetPos, 1f) - transform.position;
            rb.velocity =  new Vector2(moveTowards.x * Force, jumpForce);
            
            lookCorrectWay();
            
            yield return new WaitForSeconds(Random.Range(waitTime.x, waitTime.y));
        }
    }

    IEnumerator randomWalk(){
        while (alive){
            updateWalkMovement();
            var moveTowards = Vector3.MoveTowards(transform.position, targetPos, maxSpeedX) - transform.position;
            rb.velocity =  new Vector2(moveTowards.x, rb.velocity.y);

            lookCorrectWay();
            
            yield return new WaitForFixedUpdate(); // makes it update on a physics update
        }
    }

    IEnumerator dropLoot(List<buff> dropItems){
        yield return new WaitForEndOfFrame();
        foreach (var dropItem in dropItems)
        {
            if (dropItem.type == buffTypes.coin)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                var item = Instantiate(lootPrefab, transform.position, Quaternion.identity);
                item.GetComponent<lootItem>().StartAndAttach(dropItem);
            }
        }

        yield return new WaitForSeconds(1);
    }

    //Gets which drop should be given
    IEnumerator GetLootList(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(info.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"list\":" + www.downloadHandler.text + "}";
                getdrops(JsonUtility.FromJson<loot_dbList>(jsonString).list);

            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    void getdrops(List<loot_db> list)
    {
        var dropItems = new List<buff>();
        foreach (var drop in list)
        {
            if (drop.probability < Random.Range(0, 100)){
               switch (drop.name)
                {
                    case "elote":
                        dropItems.Add(new buff(buffTypes.maxSpeed, drop.modifier / 100, 10f));
                        break;
                    case "pan de muerto":
                        dropItems.Add(new buff(buffTypes.speed, drop.modifier / 100, 10f));
                        break;
                    case "mazapan":
                        dropItems.Add(new buff(buffTypes.damage, drop.modifier / 50, 10f));
                        break;
                    case "oblea":
                        dropItems.Add(new buff(buffTypes.dash, drop.modifier / 100, 10f));
                        break;
                    case "Borrachito":
                        dropItems.Add(new buff(buffTypes.jump, drop.modifier / 100, 10f));
                        break;
                    case "concha":
                        dropItems.Add(new buff(buffTypes.health, drop.modifier / 100, 10f));
                        break;
                    case "coin":
                        dropItems.Add(new buff(buffTypes.coin, drop.modifier, 10f));
                        break;
                }
            }
        }
        StartCoroutine(dropLoot(dropItems));
    }

    /// <summary>
    /// Kills the monster
    /// Does all the things it has to do when it dies
    /// </summary>
    private void killSelf(){
        alive = false;
        //Calls loot
        StartCoroutine(GetLootList("loot/" + id));
        var rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = false;
        var amount = 10f;
        rb.AddForce(new Vector2(Random.Range(-1,1) * amount, amount), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-1, 1) * amount);
        
        if (equippedWeapon.sprite.name == "espada")
            StartCoroutine(UpdateWeaponKills("weapons/kill-enemy/" +  manager.player_info.espada));
        else if (equippedWeapon.sprite.name == "balero")
            StartCoroutine(UpdateWeaponKills("weapons/kill-enemy/" +  manager.player_info.balero));
        else if (equippedWeapon.sprite.name == "trompo")
            StartCoroutine(UpdateWeaponKills("weapons/kill-enemy/" +  manager.player_info.trompo));

        StartCoroutine(dieDelay());
    }

    IEnumerator dieDelay() {
        yield return new WaitForSecondsRealtime(1);
        Destroy(this.gameObject);
    }

    IEnumerator UpdateWeaponKills(string EP)
    {
        // converts newUser to JSON
        string jsonData = JsonUtility.ToJson("");

        // POST request
        using (UnityWebRequest www = UnityWebRequest.Put(info.url + EP, jsonData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            // request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log("Guardado exitoso de la info del arma");
            }
            else
            {
                Debug.Log("Error en el guardado: " + www.error);
            }
        }
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

    // private void OnCollisionEnter2D(Collision2D other) 
    // {
    //     Debug.Log($"Mostro toco a {other.collider.tag}");

    //     if (other.collider.tag == "PlayerCollider")
    //     {
    //         HealthManager health_manager = other.gameObject.GetComponent<HealthManager>();
    //         health_manager.recieveDamage(damage);
            
    //         Debug.Log($"Player health: {health_manager.health}");
    //     }
    // }

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
    /// Gets called before the monster calculates the next pos it has to be in
    /// </summary>
    public abstract void updateWalkMovement();

    /// <summary>
    /// gets called whenever the player gets close enough to the monster
    /// </summary>
    public abstract void monsterHasActivated();
    
    /// <summary>
    /// gets called whenever the monster leaves the player radius
    /// </summary>
    public abstract void monsterHasDeactivated();
}


public enum MonsterTargetingType{
    jumps = 0,
    walk = 1,
    //fly = 2
}