using Redcode.Moroutines;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float remainingTime;
    
    private Ingredient _ingredient;

    public void SetIngredient(Ingredient ingredient) => _ingredient = ingredient;
    
    void Start()
    {
        
    }
}