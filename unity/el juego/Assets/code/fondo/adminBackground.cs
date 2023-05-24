using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adminBackground : MonoBehaviour
{
    public List<SpriteRenderer> backgrounds;
    public SpriteRenderer active;
    private void Start() {
        callAdmin.admin = this;
    }
    
    public void changebackground(int newsbackground){
        Debug.Log("Sirve");
        StartCoroutine("cambiar", newsbackground);
    }

    IEnumerator cambiar(int id){
        if (backgrounds[id] != active) {
            while(backgrounds[id].color.a < 0.99f ){
                backgrounds[id].color =  new Color(1f,1f,1f, backgrounds[id].color.a + 0.3f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            backgrounds[id].color =  new Color(1f,1f,1f,1f);

            while(active.color.a > 0.01f ){
                active.color =  new Color(1f,1f,1f, active.color.a - 0.3f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            active.color =  new Color(1f,1f,1f,0f);

            active = backgrounds[id];
        }
    }
    
}
