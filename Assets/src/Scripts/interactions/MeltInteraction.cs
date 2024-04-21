using System.Collections;
using UnityEngine;

public class MeltInteraction : AbstractInteraction
{
    [SerializeField] private BaseCountdown countdown;
    
    public override void MainInteraction(GameObject author)
    {
        Slot authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
        {
            slot.Put(authorSlot.Get());
            
            Ingredient ingredient = slot.GetObjectInSlot().GetComponent<Ingredient>();
            if (ingredient is not null && ingredient.ingredientData.isMeltable)
                StartCoroutine(Cook(ingredient));
        }
        
        else if (!slot.IsEmpty() && authorSlot.IsEmpty())
            authorSlot.Put(slot.Get());
    }

    public override void SecondaryInteraction(GameObject author) { }

    IEnumerator Cook(Ingredient ingredient)
    {
        float time = ingredient.ingredientData.time;
        countdown.SetTime(time);
        countdown.gameObject.SetActive(true);
        
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
        }
        
        slot.Clear();
        slot.Put(ingredient.ingredientData.meltedPrefab);
    }
}
