using UnityEngine;
using System.Collections;


public class SortingOrderScript : MonoBehaviour
{
    public const string LAYER_NAME = "TopLayer";
    public int sortingOrder = 0;
    private SpriteRenderer sprite;

    public GameObject[] Prefabs;
    public float spawnMin = 1f; //spawnar någon gång mellan 1 till 2 sek
    public float spawnMax = 2f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

       

        Spawn();

       
    }

    void Spawn()
    {
        
        Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity); // välj random något av objekten från listan och instantiera med normal (ingen)
        sortingOrder += 1;
        sprite.sortingOrder = sortingOrder;
            
        
        Invoke("Spawn", Random.Range(spawnMin, spawnMax)); //vi vill köra denna metoden igen direkt så vi invokar den igen.

    }
}