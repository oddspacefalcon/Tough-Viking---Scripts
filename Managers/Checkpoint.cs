using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hålla reda på när en spelare lämnar, träffar en checkpoint och näär en checkpoint behöver spawna en spelare
public class Checkpoint : MonoBehaviour {

    private List<IPlayerRespawnListener> _listeners;

    void Awake ()
    {
        
        _listeners = new List<IPlayerRespawnListener>();
	}

    public void PlayerHitCheckpoint()
    {
        // FLOATING TEXT visas i mitten centeredtextpositioner klassen fortsätter i PlayerHitCheckpointCo nedan (***)
        // använder coroutine då denna kommer exkeveras under loppet av fleraframes
        StartCoroutine(PlayerHitCheckpointCo(LevelManager.Instance.CurrentTimeBonus)); 
    }

    private IEnumerator PlayerHitCheckpointCo(int bonus)
    {
        // (***)
        FloatingText.Show("Checkpoint!", "CheckpointText", new CenteredTextPositioner(.5f));
        yield return new WaitForSeconds(.5f);
        FloatingText.Show(string.Format("+{0} Time Bonus!", bonus), "CheckpointText", new CenteredTextPositioner(.5f));
    }
	
	public void PlayerLeftCheckpoint()
    {

    }

    public void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform);
       

        //notifierar att varje monobehaviour som implementerar IPlayerRespaenListener att bli notifierad när spelaren respawnar på checkpointen där spelarnen dog.
        foreach (var listener in _listeners)
            listener.OnPlayerRespawnInThisCheckpoint(this, player);
    }

    public void AssignObjectToCheckpoint(IPlayerRespawnListener listener)
    {
        
        _listeners.Add(listener);
    }


}
