using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrder : MonoBehaviour {
    private GameObject go;
    public string sortingLayerName;
    private int sortingOrder;

   

    void Start()
    {
        GameObject Go = GameObject.Find("Background Spawner color");
        sortingOrder = Go.GetComponent<Renderer>().sortingOrder;
        Debug.Log("he");
        

    }

}
