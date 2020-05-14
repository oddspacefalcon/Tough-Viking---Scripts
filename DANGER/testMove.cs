using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour {


    public int speed = 4;
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Translate(Vector3.left * Time.deltaTime/speed, Space.World);
    }
}
