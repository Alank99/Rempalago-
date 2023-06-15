using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

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
    public GameObject iluminacion;
    public GameObject decoracion;
    public Cinemachine.CinemachineVirtualCamera cam;

    public Volume postProcessingVolume;
    public FilmGrain FuckedUpMeter;
    public ChromaticAberration FuckedUpIntensityAberration;
    public LensDistortion FuckedUpIntensityLens;

    public bool isInMidOfAnim;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        Debug.Log("Volume:" + PlayerPrefs.GetFloat("volume") + " ilumination:" + PlayerPrefs.GetInt("ilumination") + " decoration:" + PlayerPrefs.GetInt("decoration"));
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        if (PlayerPrefs.GetInt("ilumination") == 1)
        {
            this.GetComponentInChildren<Light2D>().intensity = 0.3f;
            this.GetComponentInChildren<Light2D>().pointLightOuterRadius = 10f;
            iluminacion.SetActive(true);
        }
        else
        {
            this.GetComponentInChildren<Light2D>().intensity = 1f;
            this.GetComponentInChildren<Light2D>().pointLightOuterRadius = 30f;
            iluminacion.SetActive(false);
        }
        if (PlayerPrefs.GetInt("decoration") == 1)
            decoracion.SetActive(true);
        else
            decoracion.SetActive(false);

        StartCoroutine(QueryData("player/stats/" + PlayerPrefs.GetInt("player_id", 2)));
        if (healthSingleton == null){
            healthSingleton = this;
        }
        else{
            Destroy(this);
        }
        health = MaxHealth;

        postProcessingVolume.profile.TryGet(out FuckedUpMeter);
        postProcessingVolume.profile.TryGet(out FuckedUpIntensityAberration);
        postProcessingVolume.profile.TryGet(out FuckedUpIntensityLens);

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

    private void updateVisualStuff(){
        healthBar.fillAmount = (float)health / 100;
        FuckedUpMeter.intensity.Override(1f - ((float)health / 100));
    }

    IEnumerator takeDamageAnim(int damage){
        if (isInMidOfAnim) yield break;
        var secondsToStop = Mathf.Clamp((float)damage / 20f, 0.3f, 3f);
        var vignetteAmount = 0.1f + secondsToStop/2f;

        // make game seem like serious stuff

        // make the game stop in 5 frames:

        isInMidOfAnim = true;

        var repetitions = 10;
        for (var i = 0; i <= repetitions; i++){
            Time.timeScale = Mathf.Lerp(1f, 0.6f, (float)i / (float)repetitions);
            FuckedUpIntensityAberration.intensity.Override(Mathf.Lerp(0.15f, 1f, (float)i / (float)repetitions));
            FuckedUpIntensityLens.intensity.Override(Mathf.Lerp(-0.2f, -0.35f, (float)i / (float)repetitions));

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSecondsRealtime(secondsToStop);

        for (var i = 0; i <= repetitions; i++){
            Time.timeScale = Mathf.Lerp(0.6f, 1f, (float)i / (float)repetitions);
            FuckedUpIntensityAberration.intensity.Override(Mathf.Lerp(1f, 0.15f, (float)i / (float)repetitions));
            FuckedUpIntensityLens.intensity.Override(Mathf.Lerp(-0.35f, -0.2f, (float)i / (float)repetitions));

            yield return new WaitForFixedUpdate();
        }

        // return tooriginal
        Time.timeScale = 1f;
        FuckedUpIntensityAberration.intensity.Override(0.15f);
        FuckedUpIntensityLens.intensity.Override(-0.2f);

        isInMidOfAnim = false;
    }

    IEnumerator getDed(){
        
        GetComponent<playerController>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        cam.Follow = transform;

        var initialSize = cam.m_Lens.OrthographicSize;
        var initialDutch = cam.m_Lens.Dutch;

        var finalSize = 2f;
        var finalDutch = -17f;

        var initialTime = Time.time;

        var executionSeconds = 1f;

        var executionPercentage = 0f;

        while (executionPercentage < 1f){
            yield return new WaitForEndOfFrame();

            cam.m_Lens.OrthographicSize = Mathf.Lerp(initialSize, finalSize, executionPercentage);
            cam.m_Lens.Dutch = Mathf.Lerp(initialDutch, finalDutch, executionPercentage);

            executionPercentage = (Time.time - initialTime) / executionSeconds;
        }

        SceneManager.LoadScene("DeadScreen"); 
    }

    public void receiveDamage(int damage){
        health -= damage;
        updateVisualStuff();

        if (health <= 0)
        {
            StartCoroutine(getDed());
        }

        StartCoroutine(takeDamageAnim(damage));

        lastHitTime = Time.time;
    }

    public void HealInternal(int healingAmount){
        health += healingAmount;
        health = Mathf.Clamp(health, 0, 100);

        updateVisualStuff();
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
        StartCoroutine(GetCheckpoint("player/last-checkpoint/" + player_info.player_id));
        health = player_info.health;
        MaxHealth = player_info.health;
        CoinCounter.instance.currentCoins = player_info.money;        
        this.GetComponent<playerController>().maxSpeedX = player_info.speed;
        change.set_damage(player_info.espada, 0);
        change.set_damage(player_info.balero, 1);
        change.set_damage(player_info.trompo, 2);
        this.GetComponent<playerController>().has_dash = player_info.dash;
        //this.transform.position = new Vector3(PlayerPrefs.GetInt("pos_x"), PlayerPrefs.GetInt("pos_y"), 0);
    }

    public void update_attack(float newattack)
    {
        player_info.attack = newattack;
    }

    public void update_speed(float newspeed)
    {
        player_info.speed = newspeed;
        this.GetComponent<playerController>().maxSpeedX = player_info.speed;
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

    public void save_to_sql(int id)
    {
        //PlayerPrefs.SetInt("checkpoint", id);
        //StartCoroutine(GetCheckpoint("player/last-checkpoint/" + player_info.player_id));
        player_info.checkpoint_id = id;
        player_info.money = CoinCounter.instance.currentCoins;
        StartCoroutine(SaveGame("player/update/" + player_info.player_id, player_info));
        StartCoroutine(SaveGame("playthroughs/update/" + player_info.player_id + "/" + (int)(Time.time - startTime) + "/0"));
        startTime = Time.time;
    }

    public void final_save()
    {
        player_info.money = CoinCounter.instance.currentCoins;
        StartCoroutine(SaveGame("player/update/" + player_info.player_id, player_info));
        StartCoroutine(SaveGame("playthroughs/update/" + player_info.player_id + "/" + (int)(Time.time - startTime) + "/1"));
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
                Debug.Log("Guardado exitoso player");
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
                Debug.Log("Guardado exitoso playthrough");
            }
            else
            {
                Debug.Log("Error en el guardado: " + www.error);
            }
        }
    }

    IEnumerator GetCheckpoint(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(info.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                string jsonString = "{\"list\":" + www.downloadHandler.text + "}";
                checkpoint check = JsonUtility.FromJson<checkpointList>(jsonString).list[0];
                this.transform.position = new Vector3(check.position_x, check.position_y, 0);
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

}