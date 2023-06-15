using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class Interact : MonoBehaviour
{
    [Header("Shop")]
    [SerializeField] GameObject tienda_ui;
    [SerializeField] GameObject vendedor;
    [Tooltip("Distance from player to interact with interactable objects")]
    [SerializeField] float distance_from_player;
    [Tooltip("Time to wait before closing dialog box")]
    [SerializeField] float wait_time;
    [SerializeField] GameObject dialog_box;
    [SerializeField] Image dialog_image;
    [SerializeField] TMPro.TextMeshProUGUI name_dialog;
    [SerializeField] TMPro.TextMeshProUGUI text_dialog;
    [Header("NPCS")]
    [SerializeField] Sprite ajolote;
    [SerializeField] Sprite alex;
    [SerializeField] Sprite Cami;

    [Header("Interact")]
    [SerializeField] GameObject note1;
    [SerializeField] GameObject cartel1;
    [SerializeField] GameObject sobre1;
    [SerializeField] GameObject cartel2;
    [SerializeField] GameObject cami;
    [SerializeField] GameObject peligro;
    [SerializeField] GameObject madre;

    [Header("Unlockables")]
    [SerializeField] GameObject dash_unlock;
    [SerializeField] GameObject trompo_unlock;
    [SerializeField] GameObject balero_unlock;


    private GameObject[] saves;

    void Start()
    {
        saves = GameObject.FindGameObjectsWithTag("SavePoint");
    }

    public void OnInteract()
    {
        //Check if player position is close to this object position
        if (Vector3.Distance(vendedor.transform.position, transform.position) < distance_from_player)
        {
            //activate tienda ui
            if (tienda_ui.activeSelf)
                tienda_ui.SetActive(false);
            else
                tienda_ui.SetActive(true);
        }

        foreach (GameObject save in saves)
        {
            if (Vector3.Distance(transform.position, save.transform.position) < distance_from_player)
            {
                this.GetComponent<HealthManager>().save_to_sql(save.GetComponent<NPC>().checkpoint_id);            

                text_dialog.text = save.GetComponent<NPC>().dialogo_npc;
                name_dialog.text = "Ajolotito";
                dialog_image.sprite = ajolote;
                if (dialog_box.activeSelf)
                    dialog_box.SetActive(false);
                else
                    dialog_box.SetActive(true);
                StartCoroutine(Waiter(dialog_box));
            }
        }

        if (Vector3.Distance(transform.position, dash_unlock.transform.position) < distance_from_player)
        {
            dash_unlock.SetActive(false);
            this.GetComponent<playerController>().has_dash = 1;
            this.GetComponent<HealthManager>().player_info.dash = 1;
            text_dialog.text = dash_unlock.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        if (Vector3.Distance(transform.position, trompo_unlock.transform.position) < distance_from_player)
        {
            trompo_unlock.SetActive(false);
            this.GetComponent<HealthManager>().update_weapon(3, 2);
            text_dialog.text = trompo_unlock.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }
        
        if (Vector3.Distance(transform.position, balero_unlock.transform.position) < distance_from_player)
        {
            balero_unlock.SetActive(false);
            this.GetComponent<HealthManager>().update_weapon(2, 1);
            text_dialog.text = balero_unlock.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        //Objetos interactivos
        if (Vector3.Distance(transform.position, note1.transform.position) < distance_from_player)
        {
            note1.SetActive(false);
            text_dialog.text = note1.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        if (Vector3.Distance(transform.position, sobre1.transform.position) < distance_from_player)
        {
            sobre1.SetActive(false);
            text_dialog.text = sobre1.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        if (Vector3.Distance(transform.position, cartel1.transform.position) < distance_from_player)
        {
            text_dialog.text = cartel1.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        if (Vector3.Distance(transform.position, cartel2.transform.position) < distance_from_player)
        {
            text_dialog.text = cartel2.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        if (Vector3.Distance(transform.position, cami.transform.position) < distance_from_player)
        {
            text_dialog.text = cami.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Cami";
            dialog_image.sprite = Cami;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        if (Vector3.Distance(transform.position, madre.transform.position) < distance_from_player)
        {
            text_dialog.text = madre.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }

        if (Vector3.Distance(transform.position, peligro.transform.position) < distance_from_player)
        {
            text_dialog.text = peligro.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Alex";
            dialog_image.sprite = alex;
            dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
        }
    }

    IEnumerator Waiter(GameObject close)
    {
        yield return new WaitForSeconds(wait_time);
        close.SetActive(false);
    }
}