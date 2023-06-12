using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    static GameController current;
    private int coins;

    public static void addCoin()
    {
        current.coins++;
    }
}
