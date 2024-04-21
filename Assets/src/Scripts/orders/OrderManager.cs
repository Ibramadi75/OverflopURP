using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private float yOffset = -0.18f;
    [SerializeField] private GameObject anOrderPrefab;
    [SerializeField] private GameManager gameManager;

    private Dictionary<int, Order> _activeOrders;
    private int _counter;
    private Transform _screenUi;

    // Start is called before the first frame update
    private void Start()
    {
        _screenUi = transform.GetChild(0);
        _activeOrders = new Dictionary<int, Order>
        {
            [0] = null
        };

        StartCoroutine(SpawnOrderPeriodically());
    }

    // Update is called once per frame
    private void Update()
    {
        if (AnyOrderExpired())
        {
            var expiredOrderKeys = new List<int>();
            var expiredOrderObjects = new List<Order>();

            foreach (var activeOrder in _activeOrders)
                if (activeOrder.Value is not null && activeOrder.Value.HasExpired())
                {
                    expiredOrderObjects.Add(activeOrder.Value);
                    expiredOrderKeys.Add(activeOrder.Key);
                }

            LoseOrders(expiredOrderKeys, expiredOrderObjects);
            RearrangeOrders();
        }
    }

    public bool LoseOrderOfRecipe(string recipeTitle)
    {
        if (AnyOrderOfRecipe(recipeTitle))
        {
            var active = IndexOfOrderRecipe(recipeTitle);
            if (active != -1)
            {
                var activeOrder = _activeOrders[active];
                Destroy(activeOrder.gameObject);
                _activeOrders[active] = null;
                RearrangeOrders();
                return true;
            }
        }

        return false;
    }

    private IEnumerator SpawnOrderPeriodically()
    {
        while (true)
        {
            var position = FindFirstAvailablePosition();
            if (position != -1)
                CreateOrder(FindFirstAvailablePosition());
            yield return new WaitForSeconds(Random.Range(6f, 12f));
        }
    }

    private void CreateOrder(int positionIndex)
    {
        var order = Instantiate(anOrderPrefab, _screenUi);
        order.transform.localPosition -= Vector3.down * yOffset * positionIndex;
        _activeOrders[positionIndex] = order.GetComponent<Order>();
    }

    private void LoseOrders(List<int> keys, List<Order> values)
    {
        keys.ForEach(key => _activeOrders[key] = null);
        values.ForEach(order => Destroy(order.gameObject));
        gameManager.RemoveMoney(_activeOrders[0].recipe.GetPrice());
    }

    private void RearrangeOrders()
    {
        for (var i = 0; i < _activeOrders.Count; i++)
            if (_activeOrders[i] is not null)
            {
                var newPosition = i - 1;
                if (newPosition >= 0)
                {
                    _activeOrders[newPosition] = _activeOrders[i];
                    _activeOrders[newPosition].transform.localPosition -= Vector3.up * yOffset;
                    _activeOrders[i] = null;
                }
            }
    }

    private bool AnyOrderExpired()
    {
        return _activeOrders.Any(activeOrder => activeOrder.Value is not null && activeOrder.Value.HasExpired());
    }

    private int FindFirstAvailablePosition()
    {
        foreach (var activeOrder in _activeOrders)
            if (activeOrder.Value is null)
                return activeOrder.Key;
        return -1;
    }

    private int IndexOfOrderRecipe(string recipeTitle)
    {
        foreach (var activeOrder in _activeOrders)
            if (activeOrder.Value is not null)
                if (activeOrder.Value.IsRecipeTitleIs(recipeTitle))
                    return activeOrder.Key;

        return -1;
    }

    private bool AnyOrderOfRecipe(string recipeTitle)
    {
        return _activeOrders.Any(activeOrder =>
            activeOrder.Value is not null && activeOrder.Value.IsRecipeTitleIs(recipeTitle));
    }
}