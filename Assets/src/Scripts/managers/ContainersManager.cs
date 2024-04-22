using System.Collections.Generic;
using UnityEngine;

public class ContainersManager : MonoBehaviour
{
    [SerializeField] private List<Slot> containers;

    private Dictionary<IngredientData, Slot> _containersInfos;

    void Start()
    {
        _containersInfos = new Dictionary<IngredientData, Slot>();
        containers.ForEach(slot =>
        {
            if (slot.GetObjectInSlot() == null) return;
            _containersInfos.Add(slot.GetObjectInSlot().GetComponent<Ingredient>().ingredientData, slot);
        });
    }
    
    public Slot GetContainerWithIngredient(Ingredient ingredient)
    {
        return _containersInfos[ingredient.ingredientData];
    }
}
