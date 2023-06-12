using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InfoTienda : MonoBehaviour
{
    public weaponList weapons;
    private List<int> espadas = new List<int>();
    private List<int> baleros = new List<int>();
    private List<int> trompos = new List<int>();

    [SerializeField] HealthManager manager;

    [Header("UI")]
    [SerializeField] TMPro.TextMeshProUGUI one;
    [SerializeField] TMPro.TextMeshProUGUI two;
    [SerializeField] TMPro.TextMeshProUGUI three;
    [SerializeField] TMPro.TextMeshProUGUI four;
    [SerializeField] TMPro.TextMeshProUGUI five;
    [SerializeField] TMPro.TextMeshProUGUI six;

    private weapon espada_vender;
    private weapon balero_vender;
    private weapon trompo_vender;

    [Header("Variables")]
    [SerializeField] int multiplicador_precio;
    [SerializeField] int precio1;
    [SerializeField] int precio2;
    [SerializeField] int precio3;

    // Start is called before the first frame update
    void Start()
    {
        one.text = "Mejora de vida\n$" + precio1;
        two.text = "Mejora de ataque\n$" + precio2;
        three.text = "Mejora de velocidad\n$" + precio3;
        StartCoroutine(QueryData("weapons"));
    }

    void get_weapons()
    {
        //Separate weapon types
        for (int i = 0; i < weapons.list.Count; i++)
        {
            if (weapons.list[i].type_id == 1 && weapons.list[i].weapon_id > manager.player_info.espada)
                espadas.Add(i);
            if (weapons.list[i].type_id == 2 && weapons.list[i].weapon_id > manager.player_info.balero)
                baleros.Add(i);
            if (weapons.list[i].type_id == 3 && weapons.list[i].weapon_id > manager.player_info.trompo)
                trompos.Add(i);
        }
        //Get random number in weapons
        int a = espadas[Random.Range(0, espadas.Count)];
        int b = baleros[Random.Range(0, baleros.Count)];
        int c = trompos[Random.Range(0, trompos.Count)];
        Debug.Log("a:" + a + "b:" + b + "c:" + c);
        espada_vender = weapons.list[a];
        balero_vender = weapons.list[b];
        trompo_vender = weapons.list[c];
        four.text = espada_vender.name + "\n$" + espada_vender.damage * 10;
        five.text = balero_vender.name + "\n$" + balero_vender.damage * 10;
        six.text = trompo_vender.name + "\n$" + trompo_vender.damage * 10;
    }

    IEnumerator QueryData(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(info.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                string jsonString = "{\"list\":" + www.downloadHandler.text + "}";
                weapons = JsonUtility.FromJson<weaponList>(jsonString);
                get_weapons();
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    private bool try_buy(int precio)
    {
        if (CoinCounter.instance.currentCoins > precio)
        {
            CoinCounter.instance.IncreaseCoins(precio * -1);
            return true;
        }
        Debug.Log("No tienes suficientes monedas");
        return false;
    }

    public void buy(int button_id)
    {
        switch (button_id)
        {
            case 1:
                if (try_buy(precio1))
                    manager.MaxHealth += 10;
                    manager.health = manager.MaxHealth;
                break;
            case 2:
                if (try_buy(espada_vender.damage * multiplicador_precio))
                    manager.update_weapon(espada_vender.weapon_id, 1);
                break;
            case 3:
                if (try_buy(precio2))
                    manager.update_attack(manager.player_info.attack + 1f);
                break;
            case 4:
                if (try_buy(balero_vender.damage * multiplicador_precio))
                    manager.update_weapon(balero_vender.weapon_id, 2);
                break;
            case 5:
                if (try_buy(precio3))
                    manager.update_speed(manager.player_info.speed + 1f);
                break;
            case 6:
                if (try_buy(trompo_vender.damage * multiplicador_precio))
                    manager.update_weapon(trompo_vender.weapon_id, 2);
                break;
        }
    }
}
