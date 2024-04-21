public class TriggerableCountdown : BaseCountdown
{
    private bool _trigger;
    
    void Update()
    {
        if (_trigger)
            ResumeMoroutine();
        else
            StopMoroutine();
    }
}