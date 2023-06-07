using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class HealthManager : MonoBehaviour
{
    [Header("Health Stuff")]
    public Image healthBar;
    public int MaxHealth = 100;
    public int health = 100;
    public float coolDownUntilNextHeal = 5;
    private float lastHitTime = 0;
    public static HealthManager healthSingleton;
    [Header("Player Info")]
    public player player_info;
    public ChangeWeapon change;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        StartCoroutine(QueryData("player/stats/" + PlayerPrefs.GetInt("player_id", 2)));
        if (healthSingleton == null){
            healthSingleton = this;
        }
        else{
            Destroy(this);
        }
        health = MaxHealth;

        StartCoroutine("passiveHeals");
    }

    IEnumerator passiveHeals(){
        while(true){
            yield return new WaitForSeconds(1);
            if (health < MaxHealth ){
                HealInternal(1);
            }

            // make it stop healing if it was hit recently
            if (Time.time - lastHitTime < coolDownUntilNextHeal){ 
                yield return new WaitForSeconds(coolDownUntilNextHeal - (int)(Time.time - lastHitTime));
            }
        }
    }

    public void receiveDamage(int damage){
        health -= damage;
        healthBar.fillAmount = (float)health / 100f;

        if (health <= 0)
        {
            SceneManager.LoadScene("DeadScreen"); // TODO: Osvald cambiar a la escena de muerte
        }

        lastHitTime = Time.time;
    }

    public void HealInternal(int healingAmount){
        health += healingAmount;
        health = Mathf.Clamp(health, 0, 100);

        healthBar.fillAmount = (float)health / 100;
    }

    IEnumerator QueryData(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(info.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"list\":" + www.downloadHandler.text + "}";
                player_info = JsonUtility.FromJson<playerList>(jsonString).list[0];
                save_info();
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    /// <summary>
    /// Guarda la información del jugador recogida del servidor
    /// </summary>
    private void save_info()
    {
        health = player_info.health;
        MaxHealth = player_info.health;
        CoinCounter.instance.currentCoins = player_info.money;
        this.GetComponent<playerController>().maxSpeedX = player_info.speed;
        change.set_damage(player_info.espada, 0);
        change.set_damage(player_info.balero, 1);
        change.set_damage(player_info.trompo, 2);
        this.GetComponent<playerController>().has_dash = player_info.dash;
    }

    public void update_weapon(int weapon_id, int type)
    {
        
        change.set_damage(weapon_id, type);
        if (type == 0)
            player_info.espada = weapon_id;
        else if (type == 1)
            player_info.balero = weapon_id;
        else if (type == 2)
            player_info.trompo = weapon_id;
    }

    public void save_to_sql()
    {
        player_info.money = CoinCounter.instance.currentCoins;
        StartCoroutine(SaveGame("player/update/" + PlayerPrefs.GetInt("player_id", 2), player_info));
        StartCoroutine(SaveGame("playthroughs/update/" + PlayerPrefs.GetInt("player_id", 2) + "/" + (int)(Time.time - startTime) + "/0"));
        startTime = Time.time;
    }

    IEnumerator SaveGame(string EP, object data)
    {
        // converts newUser to JSON
        string jsonData = JsonUtility.ToJson(data);

        // POST request
        using (UnityWebRequest www = UnityWebRequest.Put(info.url + EP, jsonData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            // request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Guardado exitoso");
            }
            else
            {
                Debug.Log("Error en el guardado: " + www.error);
            }
        }
    }

    IEnumerator SaveGame(string EP)
    {
        // POST request
        using (UnityWebRequest www = UnityWebRequest.Put(info.url + EP, ""))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            // request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Guardado exitoso");
            }
            else
            {
                Debug.Log("Error en el guardado: " + www.error);
            }
        }
    }

}