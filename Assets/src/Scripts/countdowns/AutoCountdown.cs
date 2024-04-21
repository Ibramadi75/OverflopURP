public class AutoCountdown : BaseCountdown
{
    void OnEnable() => CreateMoroutine();

    void OnDisable() => ResetSize();
}