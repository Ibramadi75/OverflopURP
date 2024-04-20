public class TriggerableCountdown : BaseCountdown
{
    private bool _trigger;
    
    void Update()
    {
        if (AreMoroutinesCompleted())
            RespawnCountdown();
        
        if (_trigger)
            StartMoroutines();
        else
            StopMoroutines();
    }
}