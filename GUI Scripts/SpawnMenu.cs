using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenu : MonoBehaviour {

    public GameObject[] Prefabs;
    public float spawnMin = 1f; //spawnar någon gång mellan 1 till 2 sek
    public float spawnMax = 2f;
   
    public bool pause = false;

    //spar musik
    public GameObject GameMusic;
    public GameObject GameMusicScript;
    public static bool inGame = false; // bool för att se om vi klickat på retry, då vill vi ej spela upp en till låt

    //public static bool _isDead = false;
 
    public void Retry()
    {
        PlayerPrefs.SetInt("InGame", 1);
        Application.LoadLevel(1);
        inGame = true;
        DontDestroyOnLoad(GameMusic); // följer med till nästa scene
        DontDestroyOnLoad(GameMusicScript);
        // DontDestroyOnLoad(Spawn_Menu);

        //vi countar att vi bytt scene och förstör ej game object.
        Counter count = GameObject.Find("SceneCounter").GetComponent<Counter>();
        int curr_count = count.NumberOfSceneChanges();
        GameObject countObj = GameObject.Find("SceneCounter");
        DontDestroyOnLoad(countObj); //förstör ej vid load
        GameObject adObj = GameObject.Find("AdManager"); //destroy on load
        Destroy(adObj);

        Player.spawnPause = false;
        pause = false;
       // GameManager.Instance.ResetPoints(0);
        //GoldCoin.Coins = 0;
      
    }

    public void BackToMainMenu()
    {
        inGame = false;
        Destroy(GameObject.Find("Music"));
        Destroy(GameObject.Find("Music Manager"));
        Application.LoadLevel(0);

        //vi countar att vi bytt scene och förstör ej game object.
        Counter count = GameObject.Find("SceneCounter").GetComponent<Counter>();
        int curr_count = count.NumberOfSceneChanges();

        GameObject countObj = GameObject.Find("SceneCounter");
        DontDestroyOnLoad(countObj); //förstör ej vid load
        GameObject adObj = GameObject.Find("AdManager"); //destroy on load
        Destroy(adObj);

        Player.spawnPause = false;
        pause = false;
        GameManager.Instance.ResetPoints(0);
        GoldCoin.Coins = 0;
    }


}
