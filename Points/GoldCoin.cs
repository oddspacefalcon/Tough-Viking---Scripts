using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour {

    public GameObject Effect;
    public int PointsToAdd = 1;
    //public AudioClip GetPoint;
    public static int Coins = 0;

    public void OnTriggerEnter2D(Collider2D other)
    {
       /* if (other.GetComponent<Player>())
        {
            if (GetPoint != null)
                AudioSource.PlayClipAtPoint(GetPoint, transform.position);
        }*/

        if (other.tag == "Player")
        {
            Coins = Coins + 1;
            Instantiate(Effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        

        
    }


   

}
