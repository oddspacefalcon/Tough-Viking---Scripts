using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIntoObj : MonoBehaviour {
    public GameObject[] Prefabs;
    public float spawnMin = 1f; //spawnar någon gång mellan 1 till 2 sek
    public float spawnMax = 2f;

    public bool pause = false;

    //public static bool _isDead = false;


    void Start()
    {
        Spawn();

    }
    private void Update()
    {
        if (Player.spawnPause == true)
        {
            pause = true;

        }

    }

    void Spawn()
    {
        if (pause == false)
        {

            var newObj = GameObject.Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);
            newObj.transform.parent = GameObject.Find("First Background Parralax").transform;
            Invoke("Spawn", Random.Range(spawnMin, spawnMax)); //vi vill köra denna metoden igen direkt så vi invokar den igen.
        }




    }
}
