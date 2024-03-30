using System.Linq;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public GameObject[] slots;
    [SerializeField] private uint maxCapacity;
    [SerializeField] bool isInfinite = false; // Defines if the slots are infinite or not.
    
    public uint GetCapacity() => maxCapacity;
    public bool IsInfinite() => isInfinite;

    void Start()
    {
        if (maxCapacity <= 0 || isInfinite)
            maxCapacity = 1;

        // Check if the slots array is not initialized from the inspector
        if (slots == null || slots.Length != maxCapacity)
        {
            GameObject[] newSlots = new GameObject[maxCapacity];
            
            // Preserve values from the old array if it's initialized
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
                    return obj;
                }
            }
        }

        return null;
    }

    public void ClearSlots()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            slots[i] = null;
        }
    }

    public bool IsEmpty()
    {
        return !slots.Any(slot => slot is not null);
    }
}
