using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InfoTienda : MonoBehaviour
{
    public weaponList weapons;
    private weaponList espadas;
    private weaponList baleros;
    private weaponList trompos;

    private int done = 0;
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
    [SerializeField] int precio_vida;
    [SerializeField] int precio_mejora;
    [SerializeField] int precio_buff;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(QueryData("get_weapons"));
    }

    void get_weapons()
    {
        //Separate weapon types
        for (int i = 0; i < weapons.list.Count; i++)
        {
            if (weapons.list[i].type_id == 1)
                espadas.list.Add(weapons.list[i]);
            if (weapons.list[i].type_id == 2)
                baleros.list.Add(weapons.list[i]);
            if (weapons.list[i].type_id == 3)
                trompos.list.Add(weapons.list[i]);
        }
        //Get random number in weapons
        int a = Random.Range(0, espadas.list.Count);
        int b = Random.Range(0, baleros.list.Count);
        int c = Random.Range(0, trompos.list.Count);
        Debug.Log("a:" + a + "b:" + b + "c:" + c);
        espada_vender = espadas.list[a];
        balero_vender = baleros.list[b];
        trompo_vender = trompos.list[c];
        four.text = espada_vender.name + "\n$" + espada_vender.damage * 10;
        five.text = balero_vender.name + "\n$" + balero_vender.damage * 10;
        six.text = trompo_vender.name + "\n$" + trompo_vender.damage * 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (done == 1)
        {
            get_weapons();
            done = 0;
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
                weapons = JsonUtility.FromJson<weaponList>(jsonString);
                done = 1;
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    public void buy(int button_id)
    {
        int precio = 0;
        switch (button_id)
        {
            case 1:
                precio = precio_vida;
                break;
            case 2:
                precio = precio_mejora;
                break;
            case 3:
                precio = precio_buff;
                break;
            case 4:
                precio = espada_vender.damage * 10;
                break;
            case 5:
                precio = balero_vender.damage * 10;
                break;
            case 6:
                precio = trompo_vender.damage * 10;
                if (CoinCounter.instance.currentCoins > precio)
                {
                    CoinCounter.instance.IncreaseCoins(precio * -1);
                }
                break;
        }

    }
}
