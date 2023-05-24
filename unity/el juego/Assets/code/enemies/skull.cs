using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull : genericMonster
{

    public HealthManager HealthManager;

    public float distance;

    public GameObject player;
    public void Update() {

        distance = Vector2.Distance(transform.position, player.transform.position);
        // Debug.Log("Distance from skull: " + distance); 
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, 1f) * Mathf.Rad2Deg;

        if (player != null && active)
            if (distance <13)
                targetPos = player.transform.position;
            else
                targetPos = transform.position;
    }

    public void Start() {
        player = GameObject.FindWithTag("Player");
        targetPos = player.transform.position;
        damage = 5;
    }

    public override void monsterHasActivated()
    {
        Debug.Log("Monstruo activado");
    }

    public override void monsterHasDeactivated()
    {
        Debug.Log("Monstruo desactivado");
    }
}