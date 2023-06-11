using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charro : genericMonster
{

    public float distance;

    public GameObject player;
    public void Update() {}
    
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