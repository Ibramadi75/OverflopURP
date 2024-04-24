using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private Order order;
    [SerializeField] private List<DeliveryInteraction> deliveryObjects;
    [SerializeField] private PlayerController playerController;
    
    private Moroutine _orderSpawnerMoroutine;
    private List<Order> _activeOrders;
    private GameManager _gameManager;

    void Awake()
    {
        _gameManager = GetComponent<GameManager>();
        _activeOrders = new List<Order>();
        _orderSpawnerMoroutine = Moroutine.Create(SpawnOrder()).Run();
    }

    void Update()
    {
        if (FindFirstAvailableDeliveryInteraction() != null)
        {
            _orderSpawnerMoroutine.Run();
        }
    }

    public void CompleteOrder(RecipeData recipeData)
    {
        foreach (Order anOrder in _activeOrders)
        {
            if (anOrder.GetRecipe().GetTitle().Equals(recipeData.title))
            {
                _gameManager.AddMoney(recipeData.price);
                RemoveOrder(anOrder);
                return;
            }
        }
    }

    private void OnAnOrderExpire(Order expiredOrder)
    {
        _activeOrders.Remove(expiredOrder);
        _gameManager.RemoveMoney(expiredOrder.GetRecipe().GetPrice());
        RemoveOrder(expiredOrder);
    }
    
    private void CreateOrder(DeliveryInteraction deliveryInteraction)
    {
        Vector3 showUpPosition = deliveryInteraction.GetShowUpPosition().position;
        Order newOrder = Instantiate(order.gameObject, showUpPosition, Quaternion.identity).GetComponent<Order>();
        newOrder.transform.parent = deliveryInteraction.transform;
        newOrder.GetComponent<LookAtTarget>().target = playerController.transform;
        _activeOrders.Add(newOrder);
        newOrder.onExpire += OnAnOrderExpire;
        deliveryInteraction.SetAvailable(false);
        newOrder.SetDeliveryInteraction(deliveryInteraction);
    }

    private void RemoveOrder(Order anOrder)
    {
        _activeOrders.Remove(anOrder);
        anOrder.StopCountdown();
        anOrder.GetDeliveryInteraction().SetAvailable(true);
        Destroy(anOrder.gameObject);
    }

    private DeliveryInteraction FindFirstAvailableDeliveryInteraction()
    {
        foreach (DeliveryInteraction deliveryInteraction in deliveryObjects)
        {
            if (deliveryInteraction.IsAvailable())
                return deliveryInteraction;
        }

        return null;
    }
    
    private IEnumerable SpawnOrder()
    {
        float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
        DeliveryInteraction deliveryInteraction = FindFirstAvailableDeliveryInteraction();
        yield return new WaitForSeconds(waitTime);
        CreateOrder(deliveryInteraction);
    }
}