using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull : genericMonster
{

    public float distance;

    public GameObject player;
    public override void updateWalkMovement() {

        distance = Vector2.Distance(transform.position, player.transform.position);
        // Debug.Log("Distance from skull: " + distance); 
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, 1f) * Mathf.Rad2Deg;

        if (player != null && active)
            if (distance <10)
                targetPos = player.transform.position;
            else
                targetPos = transform.position;
    }

    public void Start() {
        player = GameObject.FindWithTag("Player");
        targetPos = player.transform.position;
    }

    public override void monsterHasActivated()
    {
        //Debug.Log("Calavera activada");
    }

    public override void monsterHasDeactivated()
    {
        //Debug.Log("Calavera desactivada");
    }
}