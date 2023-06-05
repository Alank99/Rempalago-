using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charro : genericMonster
{

    public float distance;

    public GameObject player;
    public void Update() {

        distance = Vector2.Distance(transform.position, player.transform.position);
        
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, 1f) * Mathf.Rad2Deg;

        if (player != null && active)
            if (distance <13 && distance > 5)
                targetPos = player.transform.position;
            else if (distance < 5)
                Debug.Log("Distance from charro: " + distance);
            else
                Debug.Log("They're here");
    }
    
    public void Start() {
        player = GameObject.FindWithTag("Player");
        targetPos = player.transform.position;
        damage = 10;
    }

    public override void monsterHasActivated()
    {
        Debug.Log("Charro activado");
    }

    public override void monsterHasDeactivated()
    {
        Debug.Log("Charro desactivado");
    }
}