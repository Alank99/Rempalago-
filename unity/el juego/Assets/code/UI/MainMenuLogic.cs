using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenuCanvas");
        optionsMenu = GameObject.Find("OptionsCanvas");

        mainMenu.GetComponent<Canvas>().enabled = true;
        optionsMenu.GetComponent<Canvas>().enabled = false; 
    }

    public void StartButton()
    {
       mainMenu.GetComponent<Canvas>().enabled = false; 
       SceneManager.LoadScene("Login");
    }

    public void OptionsButton()
    {
        mainMenu.GetComponent<Canvas>().enabled = false; 
        optionsMenu.GetComponent<Canvas>().enabled = true; 
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Bye bye!");
    }

    public void ReturnToMainMenuButton()
    {
        mainMenu.GetComponent<Canvas>().enabled = true;
        optionsMenu.GetComponent<Canvas>().enabled = false;
    }
}
