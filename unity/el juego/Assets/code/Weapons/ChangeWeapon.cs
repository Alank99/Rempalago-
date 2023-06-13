using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChangeWeapon : MonoBehaviour
{
    [Header("Referencias a clases")]
    [SerializeField] ControladorTrompo trompo;
    [SerializeField] WeaponHitbox espada;
    [SerializeField] WeaponHitbox balero;

    [Header("Referencias a otros objetos")]
    [SerializeField] FatherWeapon object_espada;
    [SerializeField] FatherWeapon object_trompo;
    [SerializeField] FatherWeapon object_balero;
    [SerializeField] FatherWeaponList weapons;

    [Header("Referencias a imagenes del arma actual")]
    [SerializeField] Image arma; 
    [SerializeField] Sprite ImgTrompo;
    [SerializeField] Sprite ImgBalero;
    [SerializeField] Sprite ImgEspada;

    private int actual = 0;
    private float lastUpdate;

    void start()
    {
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
                if (type == 0)
                {
                    espada.set_damage(damage);
                    weapons.list.Add(object_espada);
                }
                else if (type == 1)
                {
                    balero.set_damage(damage);
                    weapons.list.Add(object_balero);
                }
                else if (type == 2)
                {
                    trompo.set_damage(damage);
                    weapons.list.Add(object_trompo);
                }

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
        int past_weapon = 0;
        //Si tienes menos de un segundo que cambiaste de arma, no cambies
        if (Time.time - lastUpdate < 1.0f) return;

        Vector2 direction = state.Get<Vector2>();

        if (direction.y > 0)
        {
            past_weapon = actual;
            actual++;
            if (actual > weapons.list.Count - 1)
                actual = 0;
            weapons.list[actual].activa = true;
            if (past_weapon != actual)
                weapons.list[past_weapon].activa = false;
        }
        else if (direction.y < 0)
        {
            past_weapon = actual;
            actual--;
            if (actual == -1)
                actual = weapons.list.Count - 1;
            weapons.list[actual].activa = true;
            if (past_weapon != actual)
                weapons.list[past_weapon].activa = false;
        }

        switch (actual)
        {
            case 0:
                arma.sprite = ImgEspada;
                break;
            case 1:
                arma.sprite = ImgBalero;
                break;
            case 2:
                arma.sprite = ImgTrompo;
                break;
        }
        lastUpdate = Time.time;
    }
}
