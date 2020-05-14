using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
/*
    public int score = 0;
   // private GameManager _savedPoints;


    void Start () {
         //_savedPoints = GetComponent<GameManager>();
        score = GameManager.Instance.Points/15;

        
    }

    public GUISkin Skin;

    public void OnGUI()
    {
        GUI.skin = Skin;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height)); // skapar en GUI ruta i på hela skärmen
        {
            GUILayout.BeginVertical(Skin.GetStyle("Game Over"));
            {
                GUI.Label(new Rect(Screen.width/2-40, 50, 80, 30), "Game Over!");
            }
            GUILayout.EndVertical();
            //*****************
            GUILayout.BeginVertical(Skin.GetStyle("Score"));
            {
                GUI.Label(new Rect(Screen.width / 2 - 40, 200, 80, 30), "Score:" + score);
            }
            GUILayout.EndVertical();
            //****************
            GUILayout.BeginVertical(Skin.GetStyle("Retry"));
            {
                if(GUI.Button(new Rect(Screen.width / 2 - 40, 250, 100, 50), "Retry?"))
                {
                    Application.LoadLevel(1);
                    Player.spawnPause = false;
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();

        GameManager.Instance.ResetPoints(0); // reseta poängen till vad han hade innan senaste checkpointen
    }
    */
}
