using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// Skapar en väg och ordningen (op en väg) på vilken ett objekt kan färdas
public class PathDefinition : MonoBehaviour {

    // använder oss av Ttransfor för att sätta ut pkt
    public Transform[] Points; 
    
    public IEnumerator<Transform> GetPathsEnumerator()
    {
        if (Points == null || Points.Length < 1)
            yield break;


        // Tar hand om ett objekt som åker fram och tillbaka längs vägen
        var direction = 1;
        var index = 0;
        while (true) // loopar hela tiden dvs inget break 
        { yield return Points[index]; // "execution" går till användare (objektet) som returnerar vilken punkt som denne befinner sig  på (kommer vara användbart senar i programmet

            if (Points.Length == 1) // tar hand om exception om där bara vfinns en punkt
                continue;

            if (index <= 0)  // om objektet befinner sig på första pkt så åker den frammåt 
                direction = 1;
            else if (index >= Points.Length - 1) // om objektet befinner sig på sista punkten vänder den igen
                direction = -1;

            index = index + direction; // när den kommer till nästa pkt ändras index beroende på vart den befinner sig
        }
    }

    // Rita linjer mellan punkterna 
    public void OnDrawGizmos()
    {
     
        if (Points == null || Points.Length < 2)
            return;

        var points = Points.Where(t => t != null).ToList();
        if (points.Count < 2)
            return;

        for (var i = 1; i < points.Count; i++)
        {
            Gizmos.DrawLine(points[i - 1].position, points[i].position);
        }

    }

}
