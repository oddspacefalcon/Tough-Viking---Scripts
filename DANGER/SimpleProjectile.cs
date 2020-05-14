using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : Projectile {

    public int Damage;
    public GameObject DestroyedEffect;
    public int PointsToGiveToPlayer;
    public float TimeToLive; // max tid som den kan leva innan projektilen förstör sig själv

    public void Update()
    {
        if ((TimeToLive -= Time.deltaTime) <= 0)
        {
            DestroyProjectile();
            return;
        }


        // var update förflyttas projektile som 
        //transform.Translate((Direction + new Vector2(InitialVelocity.x,0)) * Speed * Time.deltaTime, Space.World);
        transform.Translate(Direction * ((Mathf.Abs(InitialVelocity.x) + Speed) * Time.deltaTime), Space.World);

    }

    /* projektiler kan förstöra andra projektiler
    public void TakeDamage(int damage, GameObject instigator)
    {
        if(PointsToGiveToPlayer  != 0)
        {
            var projectile = instigator.GetComponent<Projectile>();
            if (projectile != null && projectile.Owner.GetComponent<Player>() != null)
            {
                GameManager.Instance.AddPoints(PointsToGiveToPlayer);
                FloatingText.Show(string.Format("+{0}", PointsToGiveToPlayer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
            }
            DestroyProjectile();
        }
    }*/

    //träffar något oförstörligt
    protected override void OnColliderOther(Collider2D other)
    {
        DestroyProjectile();
    }

    //träffar något som kan ta skada och då dra av damage  från det jag träffar och försöra projektilen
    /*
    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        if (other.tag != "Player")
        {
            takeDamage.TakeDamage(Damage, gameObject);
            DestroyProjectile();
        }
        
    }*/

    //förstör projektil
    private void DestroyProjectile()
    {
        if(DestroyedEffect != null)
            Instantiate(DestroyedEffect, transform.position, transform.rotation);

        Destroy(gameObject);

        
    }
}
