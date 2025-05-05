using UnityEngine;
using System.Collections;

public class Blender : MonoBehaviour
{
    [SerializeField] private bool isBlending;
    [SerializeField] public Transform spawnPosition;

    private bool hasProcessedCurrentIngredient = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter Blend called with: " + other.name);
        if (other.CompareTag("Ingredient") && !hasProcessedCurrentIngredient)
        {
            var ingredient = other.GetComponent<Ingredient>();
            var cuttable = other.GetComponent<Cuttable>();

            Debug.Log("Ingredient: " + ingredient + ", Cuttable: " + cuttable);
            if (ingredient != null && ingredient.ingredientData.isCuttable && cuttable != null && cuttable.nextBlendableObject != null)
            {
                Debug.Log("Ingredient is cuttable and has a next blendable object: " + cuttable.nextBlendableObject.name);
                StartBlending();
                hasProcessedCurrentIngredient = true;

                Instantiate(cuttable.nextBlendableObject, spawnPosition.position, spawnPosition.rotation);
                Destroy(other.gameObject);

                StartCoroutine(ResetBlendCooldown());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            StopBlending();
        }
    }

    private IEnumerator ResetBlendCooldown()
    {
        yield return new WaitForSeconds(1f);
        hasProcessedCurrentIngredient = false;
        Debug.Log("Blend cooldown terminé");
    }

    public bool IsBlending()
    {
        return isBlending;
    }

    private void StartBlending()
    {
        isBlending = true;
        Debug.Log("Started Blending");
    }

    private void StopBlending()
    {
        isBlending = false;
        Debug.Log("Stopped Blending");
    }
}
