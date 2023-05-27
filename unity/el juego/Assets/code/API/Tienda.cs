using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] GameObject tienda_ui;
    [SerializeField] GameObject vendedor;

    void Update()
    {
    }

    public void OnInteract()
    {
        //Check if player position is close to this object position
        if (Vector3.Distance(vendedor.transform.position, transform.position) < 50f)
        {
            Debug.Log("Close Enough");
            //activate tienda ui
            tienda_ui.SetActive(true);
        }
    }
}
