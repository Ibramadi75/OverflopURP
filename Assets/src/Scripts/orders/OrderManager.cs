using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Redcode.Moroutines;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private Order order;
    [SerializeField] private GoTo npc;
    [SerializeField] private List<DeliveryInteraction> deliveryObjects;
    [SerializeField] private PlayerController playerController;

    public int orderCount = 0;
    
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
                anOrder.SetNpcToFirstPoint();
                RemoveOrder(anOrder);
                return;
            }
        }
    }

    private void OnAnOrderExpire(Order expiredOrder)
    {
        expiredOrder.SetNpcToFirstPoint();
        _activeOrders.Remove(expiredOrder);
        _gameManager.RemoveMoney(expiredOrder.GetRecipe().GetPrice());
        RemoveOrder(expiredOrder);
    }
    
    private void CreateOrder(DeliveryInteraction deliveryInteraction)
    {
        Vector3 showUpPosition = deliveryInteraction.GetShowUpPosition().position;
        Order newOrder = Instantiate(order.gameObject, showUpPosition, Quaternion.identity).GetComponent<Order>();
        newOrder.gameObject.SetActive(false);
        newOrder.transform.parent = deliveryInteraction.transform;
        newOrder.GetComponent<LookAtTarget>().target = playerController.transform;
        _activeOrders.Add(newOrder);
        newOrder.onExpire += OnAnOrderExpire;
        deliveryInteraction.SetAvailable(false);
        newOrder.SetDeliveryInteraction(deliveryInteraction);
        CreateGoToNPC(newOrder);
    }

    private void RemoveOrder(Order anOrder)
    {
        anOrder.StopCountdown();
        anOrder.GetDeliveryInteraction().SetAvailable(true);
        Destroy(anOrder.gameObject);
    }
    
    private void CreateGoToNPC(Order anOrder)
    {
        GoTo instantiatedNpc = Instantiate(npc, npc.GetSpawnPoint(), Quaternion.identity);
        instantiatedNpc.AddCheckpoint(anOrder.GetDeliveryInteraction().GetSitPosition());
        instantiatedNpc.SetOrder(anOrder);
        instantiatedNpc.onLastCheckpointReached += OnNpcReachDelivery;
        instantiatedNpc.gameObject.SetActive(true);
        anOrder.SetGoToNpc(instantiatedNpc);
    }

    private void OnNpcReachDelivery(GoTo instantiatedNpc)
    {
        instantiatedNpc.transform.DOLookAt(instantiatedNpc.GetOrder().GetDeliveryInteraction().GetShowUpPosition().transform.position, 0f, AxisConstraint.Y);
        instantiatedNpc.Sit();
        instantiatedNpc.GetOrder().gameObject.SetActive(true);
        orderCount++;
        if (orderCount >= 6)
        {
            DayNightCycle dayNightCycle = GetComponent<DayNightCycle>();
            dayNightCycle.ToggleDayNight();
        }
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