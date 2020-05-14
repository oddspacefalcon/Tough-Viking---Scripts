using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStar : MonoBehaviour, IPlayerRespawnListener {

    public GameObject Effect;
    public int PointsToAdd = 10;
    public int PointsToAddtxt = 50;
    public AudioClip GetPoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            
            if (GetPoint != null)
                AudioSource.PlayClipAtPoint(GetPoint, transform.position);
        }

        //Debug.Log("Staaar");
        if (other.GetComponent<Player>() == null) // om ingen spelare trääffar returnera (exit
            return;

        GameManager.Instance.AddPoints(PointsToAdd); // addera poäng till objektet
        Instantiate(Effect, transform.position, transform.rotation); // sätta igång vår prefab som vi satte i effect fältet, på platsen och rotationen som stjärnan 

        gameObject.SetActive(false); // sätta inaktiv om man träffar stjärnan

        //FLOATING USAGE TEXT NEDAN
        // skapar först texten vi vill ska visas, nästa syle namn (kontrollera "presentationen" för hur texten ska visas.s slutligen specifierar vi textpositioneraren (gå upp under 1.5 sek och 50 pixlar/sek)
        FloatingText.Show(string.Format("+{0}", PointsToAddtxt), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
    }


     public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
       
        gameObject.SetActive(true);
    }
}
