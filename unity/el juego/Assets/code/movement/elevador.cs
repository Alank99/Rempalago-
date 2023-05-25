using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevador : MonoBehaviour
{
   public Animator anim;

   private void Start() {
         anim = GetComponent<Animator>();
   }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("triggered" + other.gameObject.tag);
        if(other.gameObject.tag == "Player"){
            Debug.Log("Starting...");
            anim.SetTrigger("Iniciador");
        }
    }
    
}
