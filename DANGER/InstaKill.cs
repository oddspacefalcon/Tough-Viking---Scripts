 using UnityEngine;
using System.Collections;

public class InstaKill : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
       
        var Player = other.GetComponent<Player>();
        if (Player == null)
            return;
        LevelManager.Instance.KillPlayer();
    }
}
