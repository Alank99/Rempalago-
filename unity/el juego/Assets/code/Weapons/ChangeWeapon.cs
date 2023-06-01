using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChangeWeapon : MonoBehaviour
{
    [Header("Referencias a clases")]
    [SerializeField] ControladorTrompo Trompo;
    private int trompo_dmg;
    [SerializeField] Balero Balero;
    private int balero_dmg;
    [SerializeField] Espada Espada;
    private int espada_dmg;

    public List<FatherWeapon> lista = new List<FatherWeapon>();

    [Header("Referencias a imagenes del arma actual")]
    [SerializeField] Image arma; 
    [SerializeField] Sprite ImgTrompo;
    [SerializeField] Sprite ImgBalero;
    [SerializeField] Sprite ImgEspada;

    private int actual = 2;
    private float lastUpdate;


    void start()
    {
        lista.Add(Trompo);
        lista.Add(Balero);
        lista.Add(Espada);
        lastUpdate = Time.time;
        arma.sprite = ImgEspada;
    }

    public void set_damage(int weapon_id, int type)
    {
        if (weapon_id > 0)
            StartCoroutine(QueryData("weapons/" + weapon_id, type));
    }

    IEnumerator QueryData(string EP, int type)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(info.url + EP))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"list\":" + www.downloadHandler.text + "}";
                int damage = JsonUtility.FromJson<weaponList>(jsonString).list[0].damage;
                if (type == 1) espada_dmg = damage;
                else if (type == 2) balero_dmg = damage;
                else if (type == 3) trompo_dmg = damage;
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    /// <summary>
    /// Cambia el arma actual del jugador
    /// </summary>
    private void OnCambiarArma(InputValue state){
        //Si tienes menos de un segundo que cambiaste de arma, no cambies
        if (Time.time - lastUpdate < 1.0f) return;

        Vector2 direction = state.Get<Vector2>();

        if (direction.y > 0)
        {
            lista[actual].activa = false;
            actual++;
            if (actual == 3)
                actual = 0;
            lista[actual].activa = true;
        }
        else if (direction.y < 0)
        {
            lista[actual].activa = false;
            actual--;
            if (actual == -1)
                actual = 3;
            lista[actual].activa = true;
        }

        switch (actual)
        {
            case 0:
                arma.sprite = ImgBalero;
                break;
            case 1:
                arma.sprite = ImgEspada;
                break;
            case 2:
                arma.sprite = ImgTrompo;
                break;
        }
            lastUpdate = Time.time;
    }
}
