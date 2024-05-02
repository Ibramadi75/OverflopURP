using UnityEngine;

public class DeliveryInteraction : AbstractInteraction
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private Transform showUpPosition;
    [SerializeField] private Transform sitPosition;

    private bool _isAvailable = true;

    public bool IsAvailable()
    {
        return _isAvailable;
    }

    public void SetAvailable(bool isAvailable)
    {
        _isAvailable = isAvailable;
    }
    
    public Transform GetShowUpPosition()
    {
        return showUpPosition;
    }

    public Transform GetSitPosition()
    {
        return sitPosition;
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

    protected override void WhileCountdownRunning()
    {
    }

    private bool Deliver(GameObject objectToDeliver)
    {
        var ingredient = objectToDeliver.GetComponent<Ingredient>();
        var recipe = ingredient.ingredientData.recipes[0];
        if (recipe is not null)
        {
            orderManager.CompleteOrder(recipe);
            return true;
        }

        return false;
    }
}