using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Tienda : MonoBehaviour
{
    [SerializeField] GameObject tienda_ui;
    [SerializeField] GameObject vendedor;

    public weaponList weapons;

    void Start()
    {
        GetWeapons();
    }

    public void GetWeapons()
    {
        var weaponsCall = ConnectAPI<weaponList>.get("get_weapons");
        //weaponsCall.Wait();
        //weapons = weaponsCall.Result;
        Debug.Log(weaponsCall);
        //PlayerPrefs.SetInt("myUserId", 1);
    }

    void Update()
    {
    }

    public void OnInteract()
    {
        //Check if player position is close to this object position
        if (Vector3.Distance(vendedor.transform.position, transform.position) < 50f)
        {
            //activate tienda ui
            tienda_ui.SetActive(true);
        }
    }
}
