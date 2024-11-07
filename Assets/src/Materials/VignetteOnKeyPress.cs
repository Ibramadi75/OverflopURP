using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteOnKeyPress : MonoBehaviour
{
    public Volume volume; // Référence au volume dans la scène
    private Vignette vignetteEffect;
    private float targetIntensity = 1f; // Intensité maximale du vignettage (noir total)
    private float vignetteSpeed = .5f; // Vitesse d'augmentation de l'intensité

    void Start()
    {
        // Récupère l'effet Vignette du volume
        if (volume.profile.TryGet<Vignette>(out vignetteEffect))
        {
            vignetteEffect.intensity.value = 0f; // Initialement, l'intensité est nulle
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && vignetteEffect != null)
        {
            // Commence à augmenter l'intensité du vignettage quand E est pressé
            StartCoroutine(IncreaseVignetteIntensity());
        }
    }

    private System.Collections.IEnumerator IncreaseVignetteIntensity()
    {
        while (vignetteEffect.intensity.value < targetIntensity)
        {
            vignetteEffect.intensity.value += vignetteSpeed * Time.deltaTime;
            yield return null;
        }
    }
}