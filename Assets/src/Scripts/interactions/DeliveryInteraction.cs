using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


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
        // Obsolète pour VR si OnTriggerEnter prend le relai
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("food")) return;

        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null) return;

        Debug.Log("DeliveryInteraction: Ingredient found: " + ingredient.ingredientData.title);

        if (!ingredient.ingredientData.isDeliverable) return;
        Debug.Log("DeliveryInteraction: Ingredient is deliverable: " + ingredient.ingredientData.title);

        // On tente de livrer
        if (Deliver(other.gameObject))
        {
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
                grab.enabled = false;

            Debug.Log("DeliveryInteraction: Delivered " + ingredient.ingredientData.title);
        }
    }

    public override void SecondaryInteraction() { }

    protected override void OnCountdownComplete() { }

    protected override void WhileCountdownRunning() { }

    private bool Deliver(GameObject objectToDeliver)
    {
        var ingredient = objectToDeliver.GetComponent<Ingredient>();
        if (ingredient == null || ingredient.ingredientData.recipes == null || ingredient.ingredientData.recipes.Count == 0)
            return false;

        var recipe = ingredient.ingredientData.recipes[0];
        if (recipe == null)
            return false;

        orderManager.CompleteOrder(recipe);
        return true;
    }
}
