using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreValue : MonoBehaviour {

    // Score variables
    public Text Scoretext;
    public Text highScoreText;
    public static int Score;

    //Coin variables 
    public Text Cointext;
    public Text TotalCointext;
    private int CoinsInCurrentGame;
    private int tempTotalCoin;

    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        TotalCointext.text = PlayerPrefs.GetInt("TotalCoin").ToString();
        tempTotalCoin = PlayerPrefs.GetInt("TotalCoin");
        Debug.Log(tempTotalCoin);
    }

    private void Update()
    {
        Score = GameManager.Instance.Points / 15;
        Scoretext.text = Score.ToString();

        CoinsInCurrentGame = GoldCoin.Coins;
        Cointext.text = CoinsInCurrentGame.ToString();

        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Score);
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
  
        TotalCointext.text = PlayerPrefs.GetInt("TotalCoin").ToString();
        
    }


   
}
