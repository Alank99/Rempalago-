using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public Toggle ilumToggle;
    public Toggle decorToggle;
    // Start is called before the first frame update
    void Start()
    {
        ilumToggle.onValueChanged.AddListener((value) =>{SetIlumination(value);});
        decorToggle.onValueChanged.AddListener((value) =>{SetDecoration(value);});
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
        PlayerPrefs.SetInt("ilumination", 1);
        PlayerPrefs.SetInt("decoration", 1);
        PlayerPrefs.SetFloat("volume", 1f);
        PlayerPrefs.Save();
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

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }

    public void SetIlumination(bool value)
    {
        PlayerPrefs.SetInt("ilumination", value? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetDecoration(bool value)
    {
        PlayerPrefs.SetInt("decoration", value? 1 : 0);
        PlayerPrefs.Save();
    }
}
