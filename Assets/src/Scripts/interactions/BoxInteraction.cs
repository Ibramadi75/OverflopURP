using UnityEngine;

public class BoxInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Slot authorSlot = author.GetComponent<Slot>();
        if (!slot.IsEmpty() && authorSlot.IsEmpty())
            authorSlot.Put(slot.Get());
    }

    public override void SecondaryInteraction(GameObject author) { }
}