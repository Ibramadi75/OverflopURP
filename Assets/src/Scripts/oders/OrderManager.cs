using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private float yOffset = -0.18f;
    [SerializeField] private GameObject anOrderPrefab;

    private List<GameObject> _activeOrders;
    private Transform _screenUi;
    private int _counter;
    
    // Start is called before the first frame update
    void Start()
    {
        _screenUi = transform.GetChild(0);
        _activeOrders = new List<GameObject>();
        for (int i = 0; i < 4; i++) CreateOrder(i);
    }

    // Update is called once per frame
    void Update()
    {
        if (AnyOrderExpired())
        {
            List<GameObject> expiredOrders = GetExpiredOrders();
            _activeOrders.RemoveAll(expiredOrders.Contains);
            expiredOrders.ForEach(Destroy);
        }
    }

    bool AnyOrderExpired() => _activeOrders.Any(activeOrder => activeOrder.GetComponent<Order>().HasExpired());

    List<GameObject> GetExpiredOrders() =>
        _activeOrders.FindAll(activeOrder => activeOrder.GetComponent<Order>().HasExpired());
    
    void CreateOrder(int position)
    {
        GameObject order = Instantiate(anOrderPrefab, _screenUi);
        order.transform.localPosition -= Vector3.down * yOffset * position;
        _activeOrders.Add(order);
    }
}
