using UnityEngine;

public class Assembler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;
        var ingredient = GetComponent<Ingredient>();
        var otherIngredient = other.GetComponent<Ingredient>();
        if (!ingredient.ingredientData.isAssemblable || !otherIngredient.ingredientData.isAssemblable) return;
        if (otherIngredient.ingredientData.canAssembleWith.title != ingredient.ingredientData.title) return;
        Instantiate(ingredient.ingredientData.assembledPrefab, transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
