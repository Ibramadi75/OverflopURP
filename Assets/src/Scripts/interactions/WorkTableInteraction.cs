using System;
using UnityEngine;

public class WorkTableInteraction : AbstractInteraction
{
    private AudioSource _audioSource;

    void Awake() => _audioSource = GetComponent<AudioSource>();
    
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();
        Slots slots = GetComponent<Slots>();

        if (slots.IsEmpty() && !authorSlot.IsEmpty())
        {
            // Place(author, gameObject);
            slots.Store(authorSlot.Retrieve());
        }else if (!slots.IsEmpty() && authorSlot.IsEmpty())
        {
            // Place(gameObject, author);
            authorSlot.Store(slots.Retrieve());
        }
    }

    public override void SecondaryInteraction(GameObject author)
    {
        if (slots.GetCapacity() == 1 && !slots.IsEmpty())
        {
            GameObject objectToCut = slots.slots[0];
            Ingredient ingredient = objectToCut.GetComponent<Ingredient>();
            
            Countdown countdown = objectToCut.GetComponentInChildren<Countdown>();
            Slots authorSlot = author.GetComponent<Slots>();

            if (objectToCut.CompareTag("Ingredient") 
            && ingredient is not null 
            && ingredient.ingredientData.isCuttable)
            {
                if (countdown is not null)
                {
                    countdown.InteractOn();
                    _audioSource.enabled = true;
                }
                else
                {
                    GameObject cutObject = ingredient.ingredientData.cutPrefab;
                    slots.ClearSlots();
                    slots.Store(cutObject);
                    _audioSource.enabled = false;
                }
            }
        }
    }

    void Replace(GameObject objectToCut, GameObject newObject)
    {
        slots.Store(newObject);
        GameObject instantiatedObject = Instantiate(newObject, GetTopPosition(newObject, gameObject), Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        Destroy(objectToCut.gameObject);
    }

    void Update()
    {
    }
}
