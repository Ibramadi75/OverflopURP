using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour
{
    [SerializeField] private bool isCutting;

    private bool hasCutCurrentIngredient = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && !hasCutCurrentIngredient)
        {
            var ingredient = other.GetComponent<Ingredient>();
            var cuttable = other.GetComponent<Cuttable>();

            if (ingredient != null && ingredient.ingredientData.isCuttable && cuttable != null && cuttable.nextCuttableObject != null)
            {
                StartCutting();
                hasCutCurrentIngredient = true;

                Instantiate(cuttable.nextCuttableObject, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);

                StartCoroutine(ResetCutCooldown()); // Lancement du timer
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            StopCutting();
            hasCutCurrentIngredient = false; // Réinitialiser le cooldown si on sort de la zone de découpe
        }
    }

    private IEnumerator ResetCutCooldown()
    {
        yield return new WaitForSeconds(1f);
        hasCutCurrentIngredient = false;
        Debug.Log("Cooldown terminé, prêt à découper à nouveau");
    }

    public bool IsCutting()
    {
        return isCutting;
    }

    private void StartCutting()
    {
        isCutting = true;
        Debug.Log("Started Cutting");
    }

    private void StopCutting()
    {
        isCutting = false;
        Debug.Log("Stopped Cutting");
    }
}
