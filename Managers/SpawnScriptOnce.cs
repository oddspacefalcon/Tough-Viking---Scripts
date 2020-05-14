using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScriptOnce : MonoBehaviour {

    public GameObject[] Prefabs;

    // Use this for initialization
    void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Spawn () {
     
        var newObj = GameObject.Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);
        newObj.transform.parent = GameObject.Find("First Background Parralax").transform;
    }
}
