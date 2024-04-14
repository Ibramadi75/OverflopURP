using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInteraction : AbstractInteraction
{
    public AnimationManager trashAnimationManager;
    
    public override void MainInteraction(GameObject author)
    {
        Slot authorSlot = author.GetComponent<Slot>();

        if (!authorSlot.IsEmpty())
        {
            authorSlot.Clear();
            trashAnimationManager.playAnimation = true;
        }
    }

    public override void SecondaryInteraction(GameObject author) { }
}
