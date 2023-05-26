using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public playerController player;
    public int MaxHealth = 100;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5);
        }
    }

    public void recieveDamage(int damage){
        health -= damage;
        healthBar.fillAmount = health / 100;

        if (health <= 0)
        {
            SceneManager.LoadScene(0); // TODO: Osvald cambiar a la escena de muerte
        }

        Debug.Log("Health: " + health);
    }

    public void Heal(int healingAmount){
        health += healingAmount;
        health = Mathf.Clamp(health, 0, 100);

        healthBar.fillAmount = health / 100;
    }
}