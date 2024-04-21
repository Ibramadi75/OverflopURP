using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private GameObject slot;
    [SerializeField] private uint amount;
    [SerializeField] private uint maxCapacity;
    [SerializeField] private bool isInfinite;
    [SerializeField] private bool showUp;
    [SerializeField] private bool noGravity;
    [SerializeField] private Transform showUpPosition;
    [SerializeField] private GameObject showUpObject;

    void Start()
    {
        if (isInfinite)
        {
            amount = 1;
            maxCapacity = 1;
        }
    }
    
    void Update()
    {
        if (showUp && showUpObject != null && showUpPosition != null && noGravity)
            showUpObject.transform.position = showUpPosition.transform.position;
    }

    public bool IsEmpty() => slot is null || slot.Equals(null) || amount == 0;
    public GameObject GetObjectInSlot() => slot;
    public uint GetMaxCapacity() => maxCapacity;
    
    public void Put(GameObject obj, uint amount = 1)
    {
        if (IsEmpty())
        {
            slot = obj;
            if (showUp)
                ShowUp();
            
            this.amount += amount;
            return;
        }

        if (obj.CompareTag(slot.tag))
        {
            if (this.amount == maxCapacity)
                return;
            
            this.amount += amount;
        }
    }

    public GameObject Get()
    {
        GameObject get = null;
        if (!IsEmpty())
        {
            if (!isInfinite)
            {
                amount--;
                get = slot;
                
                if (amount == 0)
                    slot = null;
                
                if (showUp)
                    Destroy(showUpObject);
            }
            
            else
                get = slot;
        }
        
        return get;
    }

    public void Clear()
    {
        slot = null;
        amount = 0;
        Destroy(GetComponentInChildren<Ingredient>().gameObject);
    }

    private void ShowUp()
    {
        showUpObject = Instantiate(slot, showUpPosition.position, Quaternion.identity);
        showUpObject.transform.localScale = Vector3.one;
        showUpObject.transform.parent = transform;
        
        if (noGravity)
        {
            showUpObject.GetComponent<Rigidbody>().useGravity = false;
            showUpObject.GetComponentInChildren<Collider>().isTrigger = true;
            
        } else {
            showUpObject.GetComponent<Rigidbody>().useGravity = true;
            showUpObject.GetComponentInChildren<Collider>().isTrigger = false;
        }
        
        showUpObject.SetActive(true);
    }
}