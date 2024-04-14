using System.Linq;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public GameObject[] slots;
    [SerializeField] private uint maxCapacity;
    [SerializeField] bool isInfinite = false; // Defines if the slots are infinite or not.
    
    public uint GetCapacity() => maxCapacity;
    public bool IsInfinite() => isInfinite;
    public bool showUp = false;
    public Transform showUpPosition;
    public GameObject showUpObject;
    public bool noGravity = false;
    
    void Update()
    {
        if (showUp && showUpObject != null && showUpPosition != null && noGravity)
        {
            showUpObject.transform.position = showUpPosition.transform.position;
        }
    }
    void Start()
    {
        if (maxCapacity <= 0 || isInfinite)
            maxCapacity = 1;

        if (slots == null || slots.Length != maxCapacity)
        {
            GameObject[] newSlots = new GameObject[maxCapacity];
            
            if (slots != null)
            {
                int copyLength = Mathf.Min(slots.Length, (int)maxCapacity);
                for (int i = 0; i < copyLength; i++)
                {
                    newSlots[i] = slots[i];
                }
            }

            slots = newSlots;
        }
    }

    public bool Store(GameObject obj)
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = obj;
                ShowUp();
                return true;
            }
        }

        return false;
    }

    public GameObject Retrieve()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            if (slots[i] != null)
            {
                if (isInfinite)
                    return slots[i];
                else
                {
                    
                    var obj = slots[i];
                    slots[i] = null;
                    if (showUp)
                    {
                        showUpObject.SetActive(false);
                    }
                    return obj;
                }
            }
        }

        return null;
    }

    public void ShowUp()
    {
        if (showUp)
        {
            if (showUpPosition == null)
                showUpPosition = transform;

            GameObject newObject = slots[0];
            GameObject instantiateObject = Instantiate(newObject, showUpPosition.position, Quaternion.identity);

            instantiateObject.transform.localScale = Vector3.one;
            
            if (noGravity)
            {
                instantiateObject.GetComponent<Rigidbody>().useGravity = false;
                instantiateObject.GetComponentInChildren<Collider>().isTrigger = true;
            }else{
                instantiateObject.GetComponent<Rigidbody>().useGravity = true;
                instantiateObject.GetComponentInChildren<Collider>().isTrigger = false;
            }

            showUpObject = instantiateObject;
            showUpObject.SetActive(true);
            instantiateObject.transform.SetParent(transform);
            slots[0] = instantiateObject;
        }
    }

    public void ClearSlots()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            slots[i] = null;
        }

        Destroy(GetComponentInChildren<Ingredient>().gameObject);
    }

    public bool IsEmpty()
    {
        return !slots.Any(slot => slot is not null);
    }
}
