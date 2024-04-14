using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInteraction : AbstractInteraction
{
    public AnimationManager trashAnimationManager;
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();

        if (!authorSlot.IsEmpty())
        {
            authorSlot.ClearSlots();
            trashAnimationManager.playAnimation = true;
        }
    }

    public override void SecondaryInteraction(GameObject author)
    {
        MainInteraction(author);
    }
}
