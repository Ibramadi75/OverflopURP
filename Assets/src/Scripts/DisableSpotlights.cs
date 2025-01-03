using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DisableSpotlightsAndChangeScene : MonoBehaviour
{
    public string sceneName;  // Le nom de la scène à charger après l'extinction des lumières
    public Persistent persistent;
    
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
        
        // Change de scène après l'extinction des lumières
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);  // Charge la scène spécifiée
            if (persistent.day == persistent.endDay)
            {
                persistent.day = 1;
                Time.timeScale = 0;
            } else
                persistent.day++;
        }
    }
}