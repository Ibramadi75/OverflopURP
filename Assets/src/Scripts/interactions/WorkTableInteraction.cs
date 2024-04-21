using UnityEngine;

public class WorkTableInteraction : AbstractInteraction
{
    private AudioSource _audioSource;

    void Awake() => _audioSource = GetComponent<AudioSource>();

    public override void MainInteraction(GameObject author)
    {
        Slot authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
            slot.Put(authorSlot.Get());
        
        else if (!slot.IsEmpty() && authorSlot.IsEmpty())
            authorSlot.Put(slot.Get());
    }

    public override void SecondaryInteraction(GameObject author)
    {
        if (slot.GetMaxCapacity() == 1 && !slot.IsEmpty())
        {
            GameObject objectToCut = slot.GetObjectInSlot();
            Ingredient ingredientToCut = objectToCut.GetComponent<Ingredient>();

            Countdown countdown = objectToCut.GetComponentInChildren<Countdown>();
            if (objectToCut.CompareTag("Ingredient") && ingredientToCut is not null &&
                ingredientToCut.ingredientData.isCuttable)
            {
                if (countdown is not null) {
                    countdown.InteractOn();
                    _audioSource.enabled = true;
                }
                
                else
                {
                    GameObject cutObject = ingredientToCut.ingredientData.cutPrefab;
                    slot.Clear();
                    slot.Put(cutObject);
                }
            }
        }

        _audioSource.enabled = false;
    }
}
