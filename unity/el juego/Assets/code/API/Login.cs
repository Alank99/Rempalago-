using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    private int user_id;
    private playthroughList plays;

    public void check_user()
    {
        StartCoroutine(QueryData("get_playthroughs/" + user_id));
    }

    IEnumerator QueryData(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(ConnectAPI<weapon>.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"users\":" + www.downloadHandler.text + "}";
                plays = JsonUtility.FromJson<playthroughList>(jsonString);
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }
}
