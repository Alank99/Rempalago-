using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Tienda : MonoBehaviour
{
    [SerializeField] GameObject tienda_ui;
    [SerializeField] GameObject vendedor;

    void Start()
    {

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
