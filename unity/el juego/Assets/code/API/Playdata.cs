using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class player
{
    public int player_id;
    public int checkpoint_id;
    public int money;
    public int health;
    public float attack;
    public float speed;
    public int espada;
    public int balero;
    public int trompo;
    public int dash;

}

[System.Serializable]
public class playerList
{
    public List<player> players;
}

public class Playdata : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] string EP;
    [SerializeField] Text errorText;

    private playerList allplayers;
    public player myPlayer;

    void Start()
    {
        GetPlayerData();
    }

    public void GetPlayerData(){
        StartCoroutine(QueryPlayerData());
    }

    IEnumerator QueryPlayerData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"players\":" + www.downloadHandler.text + "}";
                allplayers = JsonUtility.FromJson<playerList>(jsonString);
                myPlayer = allplayers.players[0];
                if (errorText != null) errorText.text = "";
            } else {
                Debug.Log("Error: " + www.error);
                if (errorText != null) errorText.text = "Error: " + www.error;
            }
        }
    }
}