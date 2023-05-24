using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public playerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5);
        }
    }

    public void TakeDamage(float damage){
        player.healthAmount -= damage;
        healthBar.fillAmount = player.healthAmount / 100f;

        if (player.healthAmount <= 0)
        {
            SceneManager.LoadScene(0); // TODO: Osvald cambiar a la escena de muerte
        }

        Debug.Log("Health: " + player.healthAmount);
    }

    public void Heal(float healingAmount){
        player.healthAmount += healingAmount;
        player.healthAmount = Mathf.Clamp(player.healthAmount, 0, 100);

        healthBar.fillAmount = player.healthAmount / 100f;
    }
}
