using UnityEngine;

public class Order : MonoBehaviour
{
    public delegate void OnExpire(Order expiredOrder);
    public OnExpire onExpire;
    
    [SerializeField] private Countdown countdown;

    private GoTo _npc;
    private DeliveryInteraction _deliveryInteraction;
    private Recipe _recipe;

    public Recipe GetRecipe()
    {
        return _recipe;
    }

    public void SetGoToNpc(GoTo npc)
    {
        _npc = npc;
    }

    public void SetNpcToFirstPoint()
    {
        _npc.ReverseCheckpointsOrder();
        _npc.onLastCheckpointReached = null;
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