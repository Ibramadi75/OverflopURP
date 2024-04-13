using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();

        if (!authorSlot.IsEmpty())
        {
            authorSlot.ClearSlots();
        }
    }

    public override void SecondaryInteraction(GameObject author)
    {
        MainInteraction(author);
    }
}
