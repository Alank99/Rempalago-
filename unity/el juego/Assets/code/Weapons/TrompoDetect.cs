using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrompoDetect : MonoBehaviour
{
    public ControladorTrompo Player;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Trompo")){
            Player.pickup();
        }
    }
}
