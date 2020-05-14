using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateShop : MonoBehaviour {

    public GameObject[] ShopPrefabs;
    public static int NumberOfGunsInShop =3;

    void Start () {
        Populate();
	}
	
	void Populate()
    {
        GameObject newObj;

        for (int i = 0; i < NumberOfGunsInShop; i++)
        {
            newObj = (GameObject)Instantiate(ShopPrefabs[i], transform);
        }


    }
    
}
