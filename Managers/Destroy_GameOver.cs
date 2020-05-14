using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_GameOver : MonoBehaviour {

    
	// Update is called once per frame
	public void Destroyer () {
		
        Destroy(transform.parent.gameObject);
            Debug.Log("worked");
        
	}
}
