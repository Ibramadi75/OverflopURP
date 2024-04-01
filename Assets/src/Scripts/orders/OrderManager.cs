using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private float yOffset = -0.18f;
    [SerializeField] private GameObject anOrderPrefab;
    [SerializeField] private GameManager gameManager;

    private Dictionary<int, Order> _activeOrders;
    private Transform _screenUi;
    private int _counter;

    public bool LoseOrderOfRecipe(string recipeTitle)
    {
        if (AnyOrderOfRecipe(recipeTitle))
        {
            int active = IndexOfOrderRecipe(recipeTitle);
            if (active != -1)
            {
                Order activeOrder = _activeOrders[active];
                Destroy(activeOrder.gameObject);
                _activeOrders[active] = null;
                RearrangeOrders();
                return true;
            }
        }

        return false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _screenUi = transform.GetChild(0);
        _activeOrders = new Dictionary<int, Order>
        {
            [0] = null,
            [1] = null,
            [2] = null,
            [3] = null
        };

        StartCoroutine(SpawnOrderPeriodically());
    }

    // Update is called once per frame
    void Update()
    {
        if (AnyOrderExpired())
        {
            List<int> expiredOrderKeys = new List<int>();
            List<Order> expiredOrderObjects = new List<Order>();
            
            foreach (var activeOrder in _activeOrders)
            {
                if (activeOrder.Value is not null && activeOrder.Value.HasExpired())
                {
                    expiredOrderObjects.Add(activeOrder.Value);
                    expiredOrderKeys.Add(activeOrder.Key);
                }
            }

            LoseOrders(expiredOrderKeys, expiredOrderObjects);
            RearrangeOrders();
        }
    }

    IEnumerator SpawnOrderPeriodically()
    {
        while (true)
        {
            int position = FindFirstAvailablePosition();
            if (position != -1)
                CreateOrder(FindFirstAvailablePosition());
            yield return new WaitForSeconds(3f);
        }
    }
    
    void CreateOrder(int positionIndex)
    {
        GameObject order = Instantiate(anOrderPrefab, _screenUi);
        order.transform.localPosition -= Vector3.down * yOffset * positionIndex;
        _activeOrders[positionIndex] = order.GetComponent<Order>();
    } 

    void LoseOrders(List<int> keys, List<Order> values)
    {
        keys.ForEach(key => _activeOrders[key] = null);
        values.ForEach(order => Destroy(order.gameObject));
        gameManager.RemoveTime();
    }

    void RearrangeOrders()
    {
        for (int i = 0; i < _activeOrders.Count; i++)
        {
            if (_activeOrders[i] is not null)
            {
                int newPosition = i - 1;
                if (newPosition >= 0)
                {
                    _activeOrders[newPosition] = _activeOrders[i];
                    _activeOrders[newPosition].transform.localPosition -= Vector3.up * yOffset;
                    _activeOrders[i] = null;
                }
            }
        }
    }
    
    bool AnyOrderExpired() => _activeOrders.Any(activeOrder => activeOrder.Value is not null && activeOrder.Value.HasExpired());

    int FindFirstAvailablePosition()
    {
        foreach (var activeOrder in _activeOrders)
            if (activeOrder.Value is null) return activeOrder.Key;
        return -1;
    }

    int IndexOfOrderRecipe(string recipeTitle)
    {
        foreach (var activeOrder in _activeOrders)
        {
            if (activeOrder.Value is not null)
            {
                if (activeOrder.Value.IsRecipeTitleIs(recipeTitle))
                {
                    return activeOrder.Key;
                }
            }
        }

        return -1;
    }
    
    bool AnyOrderOfRecipe(string recipeTitle) => _activeOrders.Any(activeOrder =>
        activeOrder.Value is not null && activeOrder.Value.IsRecipeTitleIs(recipeTitle));
}
