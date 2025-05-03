using UnityEngine;

public class Knife : MonoBehaviour 
{
    [SerializeField] private bool isCutting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            var ingredient = other.GetComponent<Ingredient>();
            if (ingredient != null && ingredient.ingredientData.isCuttable)
            {
                StartCutting();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            StopCutting();
        }
    }

    public bool IsCutting()
    {
        return isCutting;
    }

    private void StartCutting()
    {
        isCutting = true;
    }

    private void StopCutting()
    {
        isCutting = false;
    }
}