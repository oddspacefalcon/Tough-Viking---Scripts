using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyProjectile : MonoBehaviour
{

    public int score;
    public GameObject[] Prefabs;

    public Animator Animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {   
            //animation
            Animator.SetTrigger("dead");
            StartCoroutine(KillAndWait());


        }
    }

    public IEnumerator KillAndWait()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(transform.parent.parent.parent.parent.gameObject);
        GameManager.Instance.AddPoints(score);
        // skapar först texten vi vill ska visas, nästa syle namn (kontrollera "presentationen" för hur texten ska visas.s slutligen specifierar vi textpositioneraren (gå upp under 1.5 sek och 50 pixlar/sek)
        FloatingText.Show(string.Format("+{0}", score / 15), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));


        //Spawn COin
        Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity);
    }
}

