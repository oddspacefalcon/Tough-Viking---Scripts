using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeVisibleMenu : MonoBehaviour {

    public GameObject[] Prefabs;
    public float spawnMin = 1f; //spawnar någon gång mellan 1 till 2 sek
    public float spawnMax = 2f;

    public bool pause = false;


    private void Update()
    {
        if (Player.spawnPause == true && pause == false)
        {
            pause = true;
            Invoke("Spawn", 2);

        }

    }


    void Spawn()
    {
        if (pause == true)
        {

            var Canvas_group = gameObject.GetComponent<CanvasGroup>();

            Canvas_group.alpha = 1f;
            Canvas_group.interactable = enabled;

          if (Canvas_group.tag == "Background")
            {
                Canvas_group.alpha = 0f;
            }

        }
    }
}
