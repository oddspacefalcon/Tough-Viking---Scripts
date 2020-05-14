using System;
using UnityEngine;
using System.Collections;

[Serializable] // kunna modifiera i vår inspektor om vi har ett public field av ett annat monobehaviour av denna typ
public class ControllerParameters2D
{
    public enum JumpBehavior
    {
        CanJumpOnGround,
        CanJumpAnywhere,
        CantJump,
        DoubleJump
    }

    public Vector2 MaxVelocity = new Vector2(float.MaxValue, float.MaxValue);

    [Range(0, 90)]
    public float SlopeLimit = 30;

    public float Gravity = -25f;

    public JumpBehavior JumpRestrictions;

    public float JumpFrequency = .25f;

    public float JumpMagnitude = 12; // hur mycket kraft aderas till y komponentens hastighet vid hopp

    public float SwipeMagnitude = 12;
}