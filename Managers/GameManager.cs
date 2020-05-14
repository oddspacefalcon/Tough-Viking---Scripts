using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// används som en "bag" för att contain data och ge oss ett ställe för att resetta spelet eller ge poäng

public class GameManager {

    private static GameManager _instace;
    public static GameManager Instance { get { return _instace ??(_instace = new GameManager()); } } // returnera _instance om den inte är null om denna är det gör en ny instance av GameManager 
	public int Points { get; private set; }

    private GameManager()
    {

    }

    public void Reset()
    {
        Points = 0;
    }

    public void ResetPoints(int points)
    {
        Points = points;
    }

    public void AddPoints(float pointsToAdd)
    {
        Points += (int)pointsToAdd;
    }
}
