using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public Transform Player;

    void Update () {

        if (Player != null)
        {
            transform.position = new Vector3(Player.position.x + 15f, 0, transform.position.z);
        }
    }
}
