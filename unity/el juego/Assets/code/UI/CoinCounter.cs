using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public TMP_Text coinText;
    public int currentCoins = 0;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "x " + currentCoins.ToString();
    }

    public void IncreaseCoins(int value){
        currentCoins += value;
        coinText.text = "x " + currentCoins.ToString();
    }
}
