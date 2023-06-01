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

    [Header("Referencias a Texto")]
    [SerializeField] GameObject errorText;
    [SerializeField] GameObject loadingText;

    public void try_login()
    {
        user_id = 1;
        StartCoroutine(QueryData("playthroughs/" + user_id));
    }

    public void try_register()
    {
        StartCoroutine(RegisterUser());
    }

    public void LoadNames()
    {
        ClearContents();
        GameObject uiItem;
        for (int i=0; i < plays.list.Count; i++) {
            uiItem = Instantiate(buttonPrefab);
            // Add them to the ScollView content
            uiItem.transform.SetParent(contentTransform);

            // Set the position of each element
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2 (0, -100 * i);

            // Extract the text from the argument object
            playthrough us = plays.list[i];
            //Debug.Log("ID: " + us.id_users + " | " + us.name + " " + us.surname);

            // Set the text
            TextMeshProUGUI field = uiItem.GetComponentInChildren<TextMeshProUGUI>();
            int nivel = 0;
            if (us.espada > 0) nivel++;
            if (us.trompo > 0) nivel++;
            if (us.balero > 0) nivel++;
            field.text = "Completed?: " + us.completed + " Money: " + us.money + " Dash?: " + us.dash + " Nivel arma: " + nivel;
            // Set the callback
            Button btn = uiItem.GetComponent<Button>();
            int current_index = i;
            btn.onClick.AddListener(delegate {start_game(current_index); });
        }
    }

    private void start_game(int play)
    {
        PlayerPrefs.SetInt("user_id", user_id);
        PlayerPrefs.SetInt("player_id", plays.list[play].player_id);
        //Cargar Loading
        loadingText.SetActive(true);
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
        using (UnityWebRequest www = UnityWebRequest.Get(info.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"list\":" + www.downloadHandler.text + "}";
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
