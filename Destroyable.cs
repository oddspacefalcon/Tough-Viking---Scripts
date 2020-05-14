using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour, ITakeDamage
{
    public void TakeDamage(int damage, GameObject instigator)
    {
        Destroy(gameObject);
    }
}
