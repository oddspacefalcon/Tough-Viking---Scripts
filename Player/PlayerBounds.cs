﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kontrollera vad som händer när spelaren faller av leveln
public class PlayerBounds : MonoBehaviour {

	public enum BoundsBehaviour
    {
        Nothing, // inget händer
        Constrain, // inte kunna hoppa av
        Kill // döda spelaren 
    }

    // vilka bounds vi har
    public BoxCollider2D Bounds;
    public BoundsBehaviour Above;
    public BoundsBehaviour Bellow;
    public BoundsBehaviour Left;
    public BoundsBehaviour Right;

    private Player _player; // hålla koll på om spelarn är död eller ej
    private BoxCollider2D _boxcollider; // träffar boundsen elelr ej

    public void Start()
    {
        _player = GetComponent<Player>();
        _boxcollider = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        if (_player.IsDead)
            return;

        var colliderSize = new Vector2(
            _boxcollider.size.x * Mathf.Abs(transform.localScale.x),
            _boxcollider.size.y * Mathf.Abs(transform.localScale.y))/2; // räknar ut hur stor vår collider är
        // ta reda på vilket bound spelaren träffar

        if (Above != BoundsBehaviour.Nothing && transform.position.y + colliderSize.y > Bounds.bounds.max.y)
            ApplyBoundsBehaviour(Above, new Vector2(transform.position.x, Bounds.bounds.max.y - colliderSize.y));

        if (Bellow != BoundsBehaviour.Nothing && transform.position.y - colliderSize.y < Bounds.bounds.min.y)
            ApplyBoundsBehaviour(Bellow, new Vector2(transform.position.x, Bounds.bounds.min.y + colliderSize.y));

        if (Right != BoundsBehaviour.Nothing && transform.position.x + colliderSize.x > Bounds.bounds.max.x)
            ApplyBoundsBehaviour(Right, new Vector2(Bounds.bounds.max.x - colliderSize.x, transform.position.y));

        if (Left != BoundsBehaviour.Nothing && transform.position.x - colliderSize.x < Bounds.bounds.min.x)
            ApplyBoundsBehaviour(Left, new Vector2(Bounds.bounds.min.x + colliderSize.x, transform.position.y));

    }

    private void ApplyBoundsBehaviour(BoundsBehaviour behaviour, Vector2 constraiedPosition)
    {
        if(behaviour == BoundsBehaviour.Kill)
        {
            LevelManager.Instance.KillPlayer();
            return;
        }
        transform.position = constraiedPosition;
    }

}
