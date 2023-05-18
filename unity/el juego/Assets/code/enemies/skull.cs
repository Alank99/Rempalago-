using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull : genericMonster
{
    public GameObject player;
    private void Update() {
        if (active)
            targetPos = player.transform.position;
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
        Debug.Log("mounstro activado");
    }

    public override void monsterHasDeactivated()
    {
        Debug.Log("mounstro desactivado");
    }
}
