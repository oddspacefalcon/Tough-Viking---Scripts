using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    private int counter = 0;

    public static Counter instance;
    private void Awake()
    {
        instance = this;
    }

    public int InstantNumberOfSceneChanges()
    {
        return counter+1;
    }
    // antalet gånger man bytt scenes
    public int NumberOfSceneChanges()
    {
        counter++;
        return counter;
    }

}
