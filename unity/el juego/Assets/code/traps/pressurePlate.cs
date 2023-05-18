using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public bool isActivated = false;
    public List<string> canActivateTags;
    public GameObject gameObjectToSetActive;
    public Transform startPos;
    public Transform endPos;
    public pressurePlateObjectModes gameObjectMode;
    public pressurePlateTriggerModes triggerMode;

    public List<GameObject> triggeredBy;

    public GameObject activatedSpriteObject;
    public GameObject deactivatedSpriteObject;

    private void Start() {
        triggeredBy = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (canActivateTags.Contains(other.tag)) {
            triggeredBy.Add(other.gameObject);

            // if already active, do nothing
            if (isActivated) return;

            isActivated = true;
            activatedSpriteObject.SetActive(true);
            deactivatedSpriteObject.SetActive(false);

            switch (gameObjectMode)
            {
                case (pressurePlateObjectModes.activateObject):
                    gameObjectToSetActive.SetActive(true);
                    break;
                case (pressurePlateObjectModes.deactivateObject):
                    gameObjectToSetActive.SetActive(false);
                    break;
                case (pressurePlateObjectModes.moveObject):
                    StopAllCoroutines();
                    StartCoroutine("moveObjectTowards", endPos.position);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (canActivateTags.Contains(other.tag)) {
            triggeredBy.Remove(other.gameObject);

            // if not empty, do nothing
            if (pressurePlateTriggerModes.stayActive == triggerMode || triggeredBy.Count != 0) return;

            isActivated = false;

            activatedSpriteObject.SetActive(false);
            deactivatedSpriteObject.SetActive(true);

            switch (gameObjectMode)
            {
                case (pressurePlateObjectModes.activateObject):
                    gameObjectToSetActive.SetActive(false);
                    break;
                case (pressurePlateObjectModes.deactivateObject):
                    gameObjectToSetActive.SetActive(true);
                    break;
                case (pressurePlateObjectModes.moveObject):
                    StartCoroutine("moveObjectTowards", startPos.position);
                    break;
            }
        }
    }

    IEnumerator moveObjectTowards(Vector3 position){
        while ((gameObjectToSetActive.transform.position - position).magnitude > 0.1){
            gameObjectToSetActive.transform.position = Vector3.MoveTowards(gameObjectToSetActive.transform.position, position, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        gameObjectToSetActive.transform.position = position;
    }
}


public enum pressurePlateObjectModes
{
    activateObject = 1,
    deactivateObject = 2,
    moveObject = 3
}

public enum pressurePlateTriggerModes
{
    stayActive = 1,
    requireStand = 2
}