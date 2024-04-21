using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    protected Slot slot;

    private void Start()
    {
        slot = GetComponent<Slot>();
    }

    public abstract void MainInteraction(GameObject author);
    public abstract void SecondaryInteraction();
    protected abstract void OnCountdownComplete();
}