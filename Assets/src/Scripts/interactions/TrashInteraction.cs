using UnityEngine;

public class TrashInteraction : AbstractInteraction
{
    public AnimationManager trashAnimationManager;

    public override void MainInteraction(GameObject author)
    {
        var authorSlot = author.GetComponent<Slot>();

        if (!authorSlot.IsEmpty())
        {
            authorSlot.Clear();
            trashAnimationManager.playAnimation = true;
        }
    }

    public override void SecondaryInteraction()
    {
    }

    protected override void OnCountdownComplete()
    {
    }
}