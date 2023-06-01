using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class info
{
    public static string url = "localhost:5000/api/";
}

[System.Serializable]
public class playthrough
{
    public int player_id;
    public int playtime;
    public int completed;
    public int checkpoint_id;
    public int money;
    public int health;
    public int espada;
    public int balero;
    public int trompo;
    public int dash;
}

[System.Serializable]
public class player
{
    public int player_id;
    public int checkpoint_id;
    public int money;
    public int health;
    public float attack;
    public float speed;
    public int espada;
    public int balero;
    public int trompo;
    public int dash;
}

[System.Serializable]
public class weapon
{
    public int weapon_id;
    public string name;
    public int damage;
    public int kills;
    public int type_id;
}

[System.Serializable]
public class playthroughList
{
    public List<playthrough> list;
}

[System.Serializable]
public class playerList
{
    public List<player> list;
}

[System.Serializable]
public class weaponList
{
    public List<weapon> list;
}
