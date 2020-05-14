using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ska visa hur länge spelaren spelat, hur måmnga bunuspoäng han kan få och hur många poäng han har
public class GameHud : MonoBehaviour {

    public GUISkin Skin;

    /*private void OnDisable() //passing vår score genom playerpref till nästa scenen
    {
        PlayerPrefs.SetInt("Score", GameManager.Instance.Points / 15);

    }*/

    public void OnGUI()
    {
        GUI.skin = Skin;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height)); // skapar en GUI ruta i på hela skärmen
        {
            GUILayout.BeginVertical(Skin.GetStyle("GameHud"));
            {
                GUILayout.Label(string.Format("Points: {0}", GameManager.Instance.Points/15), Skin.GetStyle("PointsText"));
                /*var time = LevelManager.Instance.RunningTime;
                GUILayout.Label(string.Format(
                    "{0:00}:{1:00} with {2} bonus",
                    time.Minutes + (time.Hours * 60),
                    time.Seconds,
                    LevelManager.Instance.CurrentTimeBonus), Skin.GetStyle("TimeText"));*/
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }
}
