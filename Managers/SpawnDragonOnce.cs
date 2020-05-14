using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDragonOnce : MonoBehaviour
{

    public GameObject[] Prefabs;

    // Use this for initialization
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Spawn()
    {

      GameObject.Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);
    }
}

