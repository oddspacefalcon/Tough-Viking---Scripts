using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanScript : MonoBehaviour {

    public GameObject[] Prefabs;
    public float spawnMin = 1f; //spawnar någon gång mellan 1 till 2 sek
    public float spawnMax = 2f;
    
    public bool pause = false;
    private bool inside_spawn_area = false;
    private bool dont_double_spawn = false;  // OM man åker in i spawn area igen inom invoken intervallet nedan kan man få dubbel spawning...

    //public static bool _isDead = false;

    private void Update()
    {
        if (Player.spawnPause == true)
        {
            pause = true;
            
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpawnArea" && dont_double_spawn == false)
        {
            inside_spawn_area = true;
            Invoke("Spawn", Random.Range(1, 2));
            //Spawn();
        }

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "SpawnArea")
        {
            inside_spawn_area = false;

        }
    }
     
   
   
    private void Spawn()
    {
        dont_double_spawn = false;  // OM man åker in i spawn area igen inom invoken intervallet nedan kan man få dubbel spawning...
        if (pause == false && inside_spawn_area == true && dont_double_spawn == false)
        {
            Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity); // välj random något av objekten från listan och instantiera med normal (ingen)
            dont_double_spawn = true;
            Invoke("Spawn", Random.Range(spawnMin + 1, spawnMax + 2));
            //vi vill köra denna metoden igen direkt så vi invokar den igen.
        }
        else
        {
            return;
        }
         
    }


   

}
