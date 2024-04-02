using System;
using Unity.VisualScripting;
using UnityEngine;

public class BoxInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();
        Slots slots = GetComponent<Slots>();

        if (!slots.IsEmpty() && authorSlot.IsEmpty())
        {
            authorSlot.Store(slots.Retrieve());
        }
    }

    public override void SecondaryInteraction(GameObject author)
    {
        MainInteraction(author);
    }
}