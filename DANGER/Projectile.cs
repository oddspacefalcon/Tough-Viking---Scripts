using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract klass går ej att initieras (klassen är en bastyp) denna får alltså bara vara en parent till de klasser som blir instantieras eller som instantieras av unity
// alla klasser som ärver av denna får alla metoder
public abstract class Projectile : MonoBehaviour {

    public float Speed;
    public LayerMask CollisionMask;

    public GameObject Owner { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 InitialVelocity { get; private set; } // markerar hastigheten hos "ägaren" av projektilen så att vi aderar projektilens hastighet med ägarens
    
    public void Initialize(GameObject owner, Vector2 direction, Vector2 initialVelocity)
    {
       
        Owner= owner;
        Direction = direction;
        InitialVelocity= initialVelocity; 
        OnInitialized();

        transform.right = direction; // så att när han vänder sig om så skjuter han åt andra hållet
    }

    // markeras protected(bara accesable från denna klass och dess barn), viritual menas att den är overridable av dens children (kan väljas om de vill bli accessade eller ej)
    // lämnas tom så att children klasserna kan välja att implementera eller ej
    protected virtual void OnInitialized()
    {

    }

    //a) kollar om saken vi kolliderar med matchar kolliskionsmasken (i unity). 
    //b) invokar en en variety av protected virtual void metoder som vi lägger in i denna klass
    public virtual void OnTriggerEnter2D(Collider2D other)
    {

        // Layer #     Binary    Decimal
        // Layer 0 = 0000 0001 = 1
        // Layer 1 = 0000 0010 = 2
        // Layer 2 = 0000 0100 = 4
        // Layer 3 = 0000 1000 = 8
        // Layer 4 = 0001 0000 = 16

        //Layer Mask for 4,2, 0 = 0001 0101
        // is layer 2 i Layer masken ^ ? 
        //vi tar då (1 << 2) = 4. Dvs: 0000 0001 << 2 = 0000 0100
        // kör nu (0001 0101) & (0000 0100) = 0000 0100 ty &(and) returnerar bara en 1:a där båda har en 1:a


        //collisionmask är den binära representationen av en grupp lager. layer är lagrets nummer tex lager 1 (integer från 0-31 ty tot 32 layer i unity)
        if ((CollisionMask.value & (1 << other.gameObject.layer)) == 0)
        {
            OnNotCollideWith(other);
            return;
        }

        var isOwner = other.gameObject == Owner;
        if (isOwner)
        {
            OnCollideOwner();
            return;
        }

        var takeDamage = (ITakeDamage)other.GetComponent(typeof(ITakeDamage));
        if(takeDamage!= null)
        {
            OnCollideTakeDamage(other, takeDamage);
            return;
        }

        OnColliderOther(other);
    }


    protected virtual void OnNotCollideWith(Collider2D other)
    {

    }

    protected virtual void OnCollideOwner()
    {

    }
    
    //träffar något som kan ta skada och då dra av damage  från det jag träffar och försöra projektilen
    protected virtual void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {

    }

    // händer när jag träffar något som är oförstörligt tex plattform, förstör projektilen
    protected virtual void OnColliderOther(Collider2D other)
    {

    }

   

}
