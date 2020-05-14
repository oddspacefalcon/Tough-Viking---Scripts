using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToPlayer : MonoBehaviour {

    public int DamageToGive = 10;

    private Vector2
        _lastPosition, // behövs för att kunna nocka bak spelaren 
        _velocity;

    public void LateUpdate() // beräkna hastigheten för gamobjectet som detta scritp är fastsatt vid, 
    {
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime; // vi tar vår senaste position och drar bort vår nuvarande position och skalar det ned med hur många sek det tog att komma dit
        _lastPosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.TakeDamage(DamageToGive, gameObject);
        var controller = player.GetComponent<CharacterController2D>(); // äger hastighetn hos spelaren 
        var totalVelocity = controller.Velocity + _velocity; // totala hastigheten (kontrollern + våran)

        // nocka tillbaks kraft och håll
        controller.SetForce(new Vector2(-1 * Mathf.Sign(totalVelocity.x) * Mathf.Clamp(Mathf.Abs(totalVelocity.x) * 5, 10, 40),
            -1 * Mathf.Sign(totalVelocity.y) * Mathf.Clamp(Mathf.Abs(totalVelocity.y) * 5, 10, 15)));
    }

}
