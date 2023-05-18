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
            
            if(other.gameObject.tag == "Player"){
                anim.SetTrigger("Iniciador");
                
            }
    }
}
