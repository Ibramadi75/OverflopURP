using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    protected Slot slot;

    void Start()
    {
        slot = GetComponent<Slot>();
    }

    public abstract void MainInteraction(GameObject author);
    public abstract void SecondaryInteraction();
    protected abstract void OnCountdownComplete();
    protected abstract void WhileCountdownRunning();
}