using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull : genericMonster
{

    private float distance;

    public GameObject player;
    private void Update() {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, 1f) * Mathf.Rad2Deg;

        if (active)
            if (distance <10)
                targetPos = player.transform.position;
            else
                targetPos = transform.position;
    }
    private void Start() {
        player = GameObject.FindWithTag("Player");
        targetPos = player.transform.position;
    }

    public override void giveDamage(GameObject player)
    {
        Debug.Log("Le hemos pegado al jugador!!!!");
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
