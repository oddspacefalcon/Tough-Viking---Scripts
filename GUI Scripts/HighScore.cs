using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    public int Score;
    public int highScore;

    public Text _score;
    public Text _highScore;

    private void Start()
    {
        _highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }


    private void Update()
    {
        
    }
}
