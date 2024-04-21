using UnityEngine;

public class BoxInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        var authorSlot = author.GetComponent<Slot>();
        if (!slot.IsEmpty() && authorSlot.IsEmpty())
            authorSlot.Put(slot.Get());
    }

    public override void SecondaryInteraction()
    {
    }

    protected override void OnCountdownComplete()
    {
    }
}