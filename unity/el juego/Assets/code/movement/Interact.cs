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

    [Header("Unlockables")]
    [SerializeField] GameObject dash_unlock;

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

        GameObject save = GameObject.FindWithTag("SavePoint");
        if (Vector3.Distance(transform.position, save.transform.position) < distance_from_player)
        {
            this.GetComponent<HealthManager>().save_to_sql();            

            text_dialog.text = save.GetComponent<NPC>().dialogo_npc;
            name_dialog.text = "Ajolotito";
            dialog_image.sprite = ajolote;
            if (dialog_box.activeSelf)
                dialog_box.SetActive(false);
            else
                dialog_box.SetActive(true);
            StartCoroutine(Waiter(dialog_box));
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
    }

    IEnumerator Waiter(GameObject close)
    {
        yield return new WaitForSeconds(wait_time);
        close.SetActive(false);
    }
}