using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
    private int user_id;
    private int player_id = -1;
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

    [Header("Referencias a Mensajes")]
    [SerializeField] GameObject errorText;
    [SerializeField] GameObject loadingText;

    [Header("Referencias a vistas")]
    [SerializeField] GameObject scrollView;
    [SerializeField] GameObject loginView;
    [SerializeField] GameObject registerView;

    public void try_login()
    {
        if (Regex.Match(email_login.text, @"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)").Value == "")
        {
            errorText.SetActive(true);
            errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: Email invalid";
            return; 
        }
        else if (password_login.text.Length < 5)
        {
            errorText.SetActive(true);
            errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: Password invalid. Must be longer than 5 characters";
            return;
        }
        StartCoroutine(TryLoginMySQL());
    }

    public void try_register()
    {
        if (Regex.Match(email_reg.text, @"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)").Value == "")
        {
            errorText.SetActive(true);
            errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: Email invalid";
            return; 
        }
        else if (password_reg.text.Length < 5)
        {
            errorText.SetActive(true);
            errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: Password invalid. Must be longer than 5 characters";
            return;
        }
        else if (username_reg.text.Length < 5)
        {
            errorText.SetActive(true); 
            errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: Username invalid. Must be longer than 5 characters";
            return;
        }
        StartCoroutine(RegisterUser("user/new"));
        return_login();
    }

    public void try_newgame()
    {
        StartCoroutine(CreatePlayer());
    }

    public void return_login()
    {
        loginView.SetActive(true);
        scrollView.SetActive(false);
        registerView.SetActive(false);
        errorText.SetActive(false);
    }

    public void return_register()
    {
        loginView.SetActive(false);
        scrollView.SetActive(false);
        registerView.SetActive(true);
        errorText.SetActive(false);
    }

    public void return_scroll()
    {
        loginView.SetActive(false);
        scrollView.SetActive(true);
        registerView.SetActive(false);
        errorText.SetActive(false);
    }

    //Creates the buttons for the different playthroughs in the scroll view
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
            int current_index = plays.list[i].player_id;
            btn.onClick.AddListener(delegate {start_game(current_index); });
        }
    }

    private void start_game(int play)
    {
        if (play < 0)
        {
            errorText.SetActive(true);
            errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: Invalid player id given";
            return;
        }
        StartCoroutine(GetCheckpoint("player/last-checkpoint/" + play));
        PlayerPrefs.SetInt("user_id", user_id);
        PlayerPrefs.SetInt("player_id", play);
        //Cargar Loading
        loadingText.SetActive(true);
    }

    // Delete any child objects
    private void ClearContents()
    {
        foreach (Transform child in contentTransform) {
            Destroy(child.gameObject);
        }
    }

    IEnumerator TryLoginMySQL()
    {
        user newUser = new user
        {
            email = email_login.text,
            name = "name",
            password = password_login.text
        };
        
        // converts newUser to JSON
        string jsonData = JsonUtility.ToJson(newUser);

        // POST request
        using (UnityWebRequest www = UnityWebRequest.Put(info.url + "user/login", jsonData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            // request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.downloadHandler.text != "[]")
                {
                    return_scroll();
                    user_id = int.Parse(Regex.Match(www.downloadHandler.text, @"\d+").Value);
                    StartCoroutine(GetPlaythroughs("playthroughs/" + user_id));
                    Debug.Log("Login exitoso usuario:" + user_id);
                }
                else
                {
                    return_login();
                    errorText.SetActive(true);
                    errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: Invalid credentials";
                    Debug.Log("Error en el login: Invalid credentials");

                }
            }
            else
            {
                errorText.SetActive(true);
                errorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Error: " + www.error;
                Debug.Log("Error en el login: " + www.error);
            }
        }
    }

    IEnumerator GetPlaythroughs(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(info.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                string jsonString = "{\"list\":" + www.downloadHandler.text + "}";
                plays = JsonUtility.FromJson<playthroughList>(jsonString);
                LoadNames();
            }
            else {
                Debug.Log("Error: " + www.error);
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
                PlayerPrefs.SetInt("pos_x", check.position_x);
                PlayerPrefs.SetInt("pos_y", check.position_y);
                //Iniciar la nueva escena
                SceneManager.LoadScene("Primary");
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    IEnumerator RegisterUser(string EP)
    {
        // register values
        string email = email_reg.text;
        string name = username_reg.text;
        string password = password_reg.text;

        // creates a class with the new user data
        user newUser = new user
        {
            email = email,
            name = name,
            password = password
        };
        Debug.Log(newUser.name);

        // converts newUser to JSON
        string jsonData = JsonUtility.ToJson(newUser);

        // POST request
        using (UnityWebRequest www = UnityWebRequest.Put(info.url + EP, jsonData))
        {
            www.method = "POST";
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

    IEnumerator CreatePlayer()
    {
        // POST request
        using (UnityWebRequest www = UnityWebRequest.Put(info.url + "player/new", ""))
        {
            www.method = "POST";
            //www.SetRequestHeader("Content-Type", "application/json");

            // request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
                player_id = int.Parse(Regex.Match(www.downloadHandler.text, @"insertId.:(\d+)").Groups[1].Value);
                Debug.Log("Creacion exitosa Player_id=" + player_id);
                StartCoroutine(CreatePlaythrough("playthroughs/new/" + user_id + "/" + player_id));
                start_game(player_id);
            }
            else
            {
                Debug.Log("Error en la creacion del usuario: " + www.error);
            }
        }
    }

    IEnumerator CreatePlaythrough(string EP)
    {
        // POST request
        using (UnityWebRequest www = UnityWebRequest.Put(info.url + EP, ""))
        {
            www.method = "POST";
            //www.SetRequestHeader("Content-Type", "application/json");

            // request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Creacion exitosa de playthrough");
            }
            else
            {
                Debug.Log("Error en la creacion del usuario: " + www.error);
            }
        }
    }
}

