using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerEnemy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy" || other.tag == "Comet" || other.tag == "Coin")
        {
            Destroy(other.gameObject);
           
        }
  
    }
}
