using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private Order order;
    [SerializeField] private DeliveryInteraction deliveryObject;
    
    private Moroutine _orderSpawnerMoroutine;
    private List<Order> _activeOrders;
    private GameManager _gameManager;

    public void CompleteOrder(RecipeData recipeData)
    {
        foreach (Order anOrder in _activeOrders)
        {
            if (anOrder.GetRecipe().GetTitle().Equals(recipeData.title))
            {
                _gameManager.AddMoney(recipeData.price);
                _activeOrders.Remove(anOrder);
                return;
            }
        }
    }
    
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _activeOrders = new List<Order>();
        _orderSpawnerMoroutine = Moroutine.Create(SpawnOrder()).Run();
    }

    private Transform FindShowUpPosition(DeliveryInteraction deliveryInteraction)
    {
        foreach (Transform child in deliveryInteraction.transform)
        {
            if (child.CompareTag("ShowUpPosition"))
                return child;
        }

        return null;
    }

    private void OnAnOrderExpire(Order expiredOrder)
    {
        _activeOrders.Remove(expiredOrder);
        _gameManager.RemoveMoney(order.GetRecipe().GetPrice());
        _orderSpawnerMoroutine.Run();
    }
    
    private IEnumerator SpawnOrder()
    {
        if (_activeOrders.Count == 1) _orderSpawnerMoroutine.Stop();
        float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(waitTime);
        Vector3 showUpPosition = FindShowUpPosition(deliveryObject).position;
        Order newOrder = Instantiate(order.gameObject, showUpPosition, Quaternion.identity).GetComponent<Order>();
        newOrder.transform.parent = deliveryObject.transform;
        _activeOrders.Add(newOrder);
        newOrder.onExpire += OnAnOrderExpire;
    }
}