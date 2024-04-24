using UnityEngine;

public class DeliveryInteraction : AbstractInteraction
{
    [SerializeField] private OrderManager _orderManager;
    [SerializeField] private Transform showUpPosition;

    public Transform GetShowUpPosition()
    {
        return showUpPosition;
    }
    
    public override void MainInteraction(GameObject author)
    {
        var authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
            if (authorSlot.GetObjectInSlot().GetComponent<Ingredient>().ingredientData.isDeliverable)
            {
                var objectToDeliver = authorSlot.Get();
                if (!Deliver(objectToDeliver))
                    authorSlot.Put(objectToDeliver);
            }
    }

    public override void SecondaryInteraction()
    {
    }

    protected override void OnCountdownComplete()
    {
    }

    private bool Deliver(GameObject objectToDeliver)
    {
        var ingredient = objectToDeliver.GetComponent<Ingredient>();
        var recipe = ingredient.ingredientData.recipes[0];
        if (recipe is not null)
        {
            _orderManager.CompleteOrder(recipe);
            return true;
        }

        return false;
    }
}