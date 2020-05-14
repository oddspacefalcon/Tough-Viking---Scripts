public interface IPlayerRespawnListener
{
    
    // denna metoden evokas när vår spelare respawnas i nuvarande checkpointen
    void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player);

}

