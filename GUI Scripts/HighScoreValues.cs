using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreValues : MonoBehaviour {

    // Score variables
    public Text highScoreText;
    public static int Score;

    //Coin variables 
    public Text TotalCointext;
    private int tempTotalCoin;



    private void Update()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        TotalCointext.text = PlayerPrefs.GetInt("TotalCoin").ToString();
        tempTotalCoin = PlayerPrefs.GetInt("TotalCoin");

    }

}
