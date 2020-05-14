/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ska visa hur länge spelaren spelat, hur måmnga bunuspoäng han kan få och hur många poäng han har
public class HUDScript : MonoBehaviour
{

    float PlayerScore = 0;

    private void Update()
    {
        PlayerScore += Time.deltaTime;
    }

    public void IncreaseScore (int amount)
    {
        PlayerScore += amount;
    }

    private void OnDisable() //passing vår score genom playerpref till nästa scenen
    {
        PlayerPrefs.SetInt("Score", (int)PlayerScore);

    }

    public GUISkin Skin;

    public void OnGUI()
    {
       GUI.skin = Skin;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height)); // skapar en GUI ruta i på hela skärmen
        {
            GUILayout.BeginVertical(Skin.GetStyle("HUDScript"));
            {
                GUI.Label(new Rect(30, 10, 200, 50), "Score" + (int)(PlayerScore));
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }
}*/
