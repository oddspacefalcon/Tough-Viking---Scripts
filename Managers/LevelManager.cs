using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { get; private set;}

    public Player Player { get; private set; }
    public CameraController Camera { get; private set; }
    public TimeSpan RunningTime { get { return DateTime.UtcNow - _started; } }

   public int CurrentTimeBonus
    {
        get
        {
            var secondDifference = (int)(BonusCutoffSeconds - RunningTime.TotalSeconds);
            return Mathf.Max(0, secondDifference) * BonusSecondMultiplier;
        }
     }

    private List<Checkpoint> _checkpoints;
    private int _currentCheckpointIndex; // hålla reda på vilken checkpoint man är på 
    private DateTime _started; // när man startar från en checkpoint
    private int _savedPoints;

    public Checkpoint DebugSpawn; // kunna spawna på specifika punketer
    public int BonusCutoffSeconds; // max tid för som man kan ha mellan tid för att kunna få bonus
    public int BonusSecondMultiplier; // hur många sek kvar *med antal poäng som man bör få för de 

    private SpawnMenu MenuSpawn;
    public bool isDead = false;
    public GameObject[] Prefabs; // spawna guin vid död
    
    

    public void Awake()
    {
        _savedPoints = GameManager.Instance.Points;
        Instance = this;
        MenuSpawn = GetComponent<SpawnMenu>();

    }

    public void Start()
    {


        _checkpoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.transform.position.x).ToList();  //lista för alla chekpoints
        _currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

        Player = FindObjectOfType<Player>();
        Camera = FindObjectOfType<CameraController>();

        _started = DateTime.UtcNow;

        // hittar alla IPlayerRespawnListener och assigna varje sådan till den individuella checkpointen som stjärnan tillhör
        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();
        foreach (var listener in listeners) // for each för att loopa ingenom varje element i en samling
        {

            for (var i = _checkpoints.Count - 1; i >= 0; i--)
            {
                var distance = ((MonoBehaviour)listener).transform.position.x - _checkpoints[i].transform.position.x;
                if (distance < 0)
                    continue;

                _checkpoints[i].AssignObjectToCheckpoint(listener);
                break;
            }
        }

#if UNITY_EDITOR
        if (DebugSpawn != null)
            DebugSpawn.SpawnPlayer(Player);
        else if (_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#else
        if(_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#endif
    }

    public void Update()
    {
        if (isDead==false)
        {
            GameManager.Instance.AddPoints(1);
            _savedPoints = GameManager.Instance.Points;
        }
        

        // vilken chekcpoint man är på 
        var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
        if (isAtLastCheckpoint)
            return;

        // hur långt bort man är från nästa checkpoint
        var distanceToNextCheckpoint = _checkpoints[_currentCheckpointIndex + 1].transform.position.x - Player.transform.position.x;
        if (distanceToNextCheckpoint >= 0)
            return;

        _checkpoints[_currentCheckpointIndex].PlayerLeftCheckpoint();
        _currentCheckpointIndex++;
        _checkpoints[_currentCheckpointIndex].PlayerHitCheckpoint();


        // när vi träffar en checkpoint fixar poäng
        
        _started = DateTime.UtcNow;

    }

    // hanetera hoppa mellan levlar när man startar
    public void GotoNextLevel(string levelName)
    {
        
        StartCoroutine(GotoNextLevelCo(levelName));
    }

    private IEnumerator GotoNextLevelCo(string levelName)
    {
        Player.FinishLevel(); //imobiliserar spelarn
       //GameManager.Instance.AddPoints(CurrentTimeBonus); //lägg till time bonus
        FloatingText.Show("Level Complete", "CheckpointText", new CenteredTextPositioner(.2f));

        FloatingText.Show(string.Format("{0} Points!", GameManager.Instance.Points), "CheckpointText", new CenteredTextPositioner(.1f));
        yield return new WaitForSeconds(5f);
        //vill vi gå tillbaks till en annan level eller till startscreen
        if (string.IsNullOrEmpty(levelName))
            Application.LoadLevel("StartScreen");
        else
            Application.LoadLevel(levelName);

    }

    public void KillPlayer()
    {
        
        StartCoroutine(KillPlayerCo());
       // _destroySpawner.Destroyer();
        
    }
    
    public IEnumerator KillPlayerCo()
    {

        // döda spelaren, sätter så att kameran inte längre  följer och väntar i 2 sekunder
        Player.Kill();
        isDead = true;
        Camera.IsFollowing = false;
        yield return new WaitForSeconds(2f);
       
        Camera.IsFollowing = true;

        // säger till kameran att spawna spelaren om vi har en valid ceckpoint
        //if (_currentCheckpointIndex != -1)
          //  _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);

        _started = DateTime.UtcNow;
        Instantiate(Prefabs[0], transform.position, Quaternion.identity);
        
        //Application.LoadLevel(0);  

    }


}



