using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private int user_id;
    public playthroughList plays;

    [Header("Referencias para el scroll")]
    [SerializeField] Transform contentTransform;
    [SerializeField] GameObject buttonPrefab;

    [Header("Referencias a textos")]
    [SerializeField] TMPro.TextMeshProUGUI email_login;
    [SerializeField] TMPro.TextMeshProUGUI password_login;
    [SerializeField] TMPro.TextMeshProUGUI email_reg;
    [SerializeField] TMPro.TextMeshProUGUI password_reg;
    [SerializeField] TMPro.TextMeshProUGUI username_reg;

    public void try_login()
    {
        StartCoroutine(QueryData("playthroughs/1"));
        
    }

    public void try_register()
    {
        StartCoroutine(RegisterUser());
    }

    public void LoadNames()
    {
        ClearContents();
        GameObject uiItem;
        for (int i=0; i < plays.playthroughs.Count; i++) {
            uiItem = Instantiate(buttonPrefab);
            // Add them to the ScollView content
            uiItem.transform.SetParent(contentTransform);

            // Set the position of each element
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2 (0, -100 * i);

            // Extract the text from the argument object
            playthrough us = plays.playthroughs[i];
            //Debug.Log("ID: " + us.id_users + " | " + us.name + " " + us.surname);

            // Set the text
            TextMeshProUGUI field = uiItem.GetComponentInChildren<TextMeshProUGUI>();
            field.text = "ID: " + us.player_id + ", playtime: " + us.playtime + ", completed: " + us.completed;
            // Set the callback
            Button btn = uiItem.GetComponent<Button>();
            btn.onClick.AddListener(delegate {start_game(i); });
        }
    }

    private void start_game(int play)
    {
        //Iniciar la nueva escena
        SceneManager.LoadScene("Primary");
    }

    // Delete any child objects
    private void ClearContents()
    {
        foreach (Transform child in contentTransform) {
            Destroy(child.gameObject);
        }
    }

    IEnumerator QueryData(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(ConnectAPI<weapon>.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"playthroughs\":" + www.downloadHandler.text + "}";
                plays = JsonUtility.FromJson<playthroughList>(jsonString);
                LoadNames();
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

/*
    IEnumerator AddUser()
    {
        // Create the object to be sent as json
        User testUser = new User();
        testUser.name = "newGuy" + Random.Range(1000, 9000).ToString();
        testUser.surname = "Tester" + Random.Range(1000, 9000).ToString();
        //Debug.Log("USER: " + testUser);
        string jsonData = JsonUtility.ToJson(testUser);
        //Debug.Log("BODY: " + jsonData);

        // Send using the Put method:
        // https://stackoverflow.com/questions/68156230/unitywebrequest-post-not-sending-body
        using (UnityWebRequest www = UnityWebRequest.Put(url + getUsersEP, jsonData))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                Debug.Log("Response: " + www.downloadHandler.text);
                if (errorText != null) errorText.text = "";
            } else {
                Debug.Log("Error: " + www.error);
                if (errorText != null) errorText.text = "Error: " + www.error;
            }
        }
    }
*/
IEnumerator RegisterUser()
    {
        // register values
        string email = email_reg.text;
        string username = username_reg.text;
        string password = password_reg.text;

        // creates a class with the new user data
        user newUser = new user
        {
            email = email,
            name = username,
            password = password
        };

        // converts newUser to JSON
        string jsonData = JsonUtility.ToJson(newUser);

        string url = "localhost" + "api/register";

        // POST request
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            // request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Registro exitoso");
            }
            else
            {
                Debug.Log("Error en el registro: " + www.error);
            }
        }
    }
}
