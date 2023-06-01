using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherWeapon : MonoBehaviour
{
    [Tooltip("Tienes la espada?")]
    public bool activa;
    public int damage;
}

[System.Serializable]
public class FatherWeaponList
{
    public List<FatherWeapon> list;
}
