using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class playthrough
{
    public int player_id;
    public int playtime;
    public int completed;
    public int checkpoint_id;
    public int money;
    public int health;
    public int espada;
    public int balero;
    public int trompo;
    public int dash;

}

[System.Serializable]
public class playthroughList
{
    public List<playthrough> playthroughs;
}

public class ShowGames : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] string EP;
        [SerializeField] Text errorText;

    public playthroughList allplaythroughs;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(QueryPlaythroughs());
    }

    IEnumerator QueryPlaythroughs()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"players\":" + www.downloadHandler.text + "}";
                allplaythroughs = JsonUtility.FromJson<playthroughList>(jsonString);
                if (errorText != null) errorText.text = "";
            } else {
                Debug.Log("Error: " + www.error);
                if (errorText != null) errorText.text = "Error: " + www.error;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
