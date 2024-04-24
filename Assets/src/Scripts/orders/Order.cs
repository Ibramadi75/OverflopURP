using UnityEngine;

public class Order : MonoBehaviour
{
    public delegate void OnExpire(Order expiredOrder);
    public OnExpire onExpire;
    
    [SerializeField] private Countdown countdown;

    private DeliveryInteraction _deliveryInteraction;
    private Recipe _recipe;

    public Recipe GetRecipe()
    {
        return _recipe;
    }

    public void SetDeliveryInteraction(DeliveryInteraction deliveryInteraction)
    {
        _deliveryInteraction = deliveryInteraction;
    }

    public DeliveryInteraction GetDeliveryInteraction()
    {
        return _deliveryInteraction;
    }

    public void StopCountdown()
    {
        countdown.StopMoroutine();
    }

    void Start()
    {
        _recipe = GetComponent<Recipe>();

        countdown.onComplete += OnCountdownComplete;
        countdown.SetTime(_recipe.GetBaseExpiration());
        countdown.gameObject.SetActive(true);
    }

    private void OnCountdownComplete()
    {
        onExpire?.Invoke(this);
    }
}