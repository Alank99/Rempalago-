using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Primary");
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
