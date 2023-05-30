using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class playthrough: ISerializableJson
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
public class player: ISerializableJson
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
public class weapon: ISerializableJson
{
    public int weapon_id;
    public string name;
    public int damage;
    public int kills;
    public int type_id;
}

[System.Serializable]
public class playthroughList: ISerializableJson
{
    public List<playthrough> playthroughs;
}

[System.Serializable]
public class playerList: ISerializableJson
{
    public List<player> players;
}

[System.Serializable]
public class weaponList: ISerializableJson
{
    public List<weapon> weapons;
}
