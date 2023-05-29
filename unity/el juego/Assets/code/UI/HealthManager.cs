using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public int MaxHealth = 100;
    public int health = 100;

    public float coolDownUntilNextHeal = 5;

    private float lastHitTime = 0;

    public static HealthManager healthSingleton;

    // Start is called before the first frame update
    void Start()
    {
        if (healthSingleton == null){
            healthSingleton = this;
        }
        else{
            Destroy(this);
        }
        health = MaxHealth;

        StartCoroutine("passiveHeals");
    }

    IEnumerator passiveHeals(){
        while(true){
            yield return new WaitForSeconds(1);
            if (health < MaxHealth ){
                HealInternal(1);
            }

            // make it stop healing if it was hit recently
            if (Time.time - lastHitTime < coolDownUntilNextHeal){ 
                yield return new WaitForSeconds(coolDownUntilNextHeal - (int)(Time.time - lastHitTime));
            }
        }
    }

    public void receiveDamage(int damage){
        health -= damage;
        healthBar.fillAmount = (float)health / 100f;

        if (health <= 0)
        {
            SceneManager.LoadScene(0); // TODO: Osvald cambiar a la escena de muerte
        }

        lastHitTime = Time.time;
    }

    public void HealInternal(int healingAmount){
        health += healingAmount;
        health = Mathf.Clamp(health, 0, 100);

        healthBar.fillAmount = (float)health / 100;
    }

}