﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Healthbar följa efter spelaren
public class FollowObject : MonoBehaviour {

    public Vector2 Offset;
    public Transform Following;

    public void Update()
    {
        transform.position = Following.transform.position + (Vector3)Offset;
    }
}
