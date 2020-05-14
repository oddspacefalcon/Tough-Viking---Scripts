using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathedProjectileSpawner : MonoBehaviour {

    public Transform Destination;
    public PathedProjectile [] Projectile;

    public GameObject SpawnEffect; // particle system vid spawn
    public float Speed;
    public float FireRate;

    

    private float _nextShotSeconds;

    private bool setParamDone = false;


    public float spawnMin = 1f; //spawnar någon gång mellan 1 till 2 sek
    public float spawnMax = 2f;

    public bool pause = false;
    void Start()
    {

        Invoke("Spawn", Random.Range(0, 4));

    }
    private void Update()
    {
        if (Player.spawnPause == true)
        {
            pause = true;

        }

    }

    void Spawn()
    {
        if (pause == false)
        {

            var projectile = (PathedProjectile)Instantiate(Projectile[Random.Range(0, Projectile.Length)], transform.position, transform.rotation);
            projectile.Initialize(Destination, Speed); // när den skjuter sätter den destinationen och hastigheten

            Invoke("Spawn", Random.Range(spawnMin, spawnMax));
        }




    }




    /* public void Start()
     {
         _nextShotSeconds = FireRate;
     }

     public void Update()
     {
         if ((_nextShotSeconds -= Time.deltaTime) > 0)
             return;

         _nextShotSeconds = FireRate;


         var projectile = (PathedProjectile)Instantiate(Projectile[Random.Range(0,Projectile.Length)], transform.position, transform.rotation);
         projectile.Initialize(Destination, Speed); // när den skjuter sätter den destinationen och hastigheten


         //if (SpawnEffect != null) // kan välja om vi vill ha en spawn effect eller ej!
             //Instantiate(SpawnEffect, transform.position, transform.rotation);
     }*/



    public void OnDrawGizmos() // vill dra en linje som visar vart projectilen kommer att åka
    {
        if (Destination == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Destination.position);
    }
}
