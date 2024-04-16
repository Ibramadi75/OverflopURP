using UnityEngine;

public class ShopInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Debug.Log("tkt");
    }

    public override void SecondaryInteraction(GameObject author) { }
}