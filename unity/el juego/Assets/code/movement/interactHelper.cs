using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class interactHelper : MonoBehaviour
{
    GameObject canvas;
    Cinemachine.CinemachineTargetGroup targetGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetChild(0).gameObject;
        targetGroup = GameObject.Find("TargetGroup").GetComponent<Cinemachine.CinemachineTargetGroup>();
    }

    IEnumerator lerpTargetMember(float time, float start, float end, bool remove = false){
        var startTime = Time.time;
        var percentageElapsed = 0f;

        while (percentageElapsed < 1f){
            percentageElapsed = (Time.time - startTime) / time;
            targetGroup.m_Targets[1].weight = Mathf.Lerp(start, end, percentageElapsed);
            yield return new WaitForEndOfFrame();
        }
        
        if (remove)
            targetGroup.RemoveMember(transform);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PlayerCollider"){
            canvas.SetActive(true);
        }

        if(other.gameObject.tag == "PlayerRadius"){
            targetGroup.AddMember(transform, 0, 10);
            StartCoroutine(lerpTargetMember(1f, 0f, 1f));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "PlayerCollider"){
            canvas.SetActive(false);
        }


        if(other.gameObject.tag == "PlayerRadius"){
            StartCoroutine(lerpTargetMember(1f, 1f, 0f, true));
        }
    }
}
