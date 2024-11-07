using UnityEngine;
using System.Collections;

public class DisableSpotlights : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Lance la coroutine pour éteindre les spotlights après 3 secondes
            StartCoroutine(DisableSpotlightsAfterDelay(3f));
        }
    }

    IEnumerator DisableSpotlightsAfterDelay(float delay)
    {
        // Attend pendant le délai spécifié
        yield return new WaitForSeconds(delay);

        // Désactive toutes les SpotLights dans les enfants
        foreach (Transform child in transform)
        {
            Light lightComponent = child.GetComponent<Light>();
            if (lightComponent != null && lightComponent.type == LightType.Spot)
            {
                lightComponent.enabled = false;
            }
        }
    }
}