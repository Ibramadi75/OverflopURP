using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class VignetteOnKeyPress : MonoBehaviour
{
    public Volume volume; // Référence au volume dans la scène
    private Vignette vignetteEffect;
    private float targetIntensity = 1f; // Intensité maximale du vignettage (noir total)
    private float vignetteSpeed = 0.5f; // Vitesse d'augmentation de l'intensité
    public bool trigger = false; // Déclencheur pour activer

    private bool hasStarted = false; // Pour éviter de lancer la coroutine plusieurs fois

    public void ActivateTrigger()
    {
        trigger = true;
    }

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
        if (trigger && vignetteEffect != null && !hasStarted)
        {
            hasStarted = true;
            StartCoroutine(IncreaseVignetteIntensity());
        }
    }

    private System.Collections.IEnumerator IncreaseVignetteIntensity()
    {
        while (vignetteEffect.intensity.value < targetIntensity )
        {
            vignetteEffect.intensity.value += vignetteSpeed * Time.deltaTime;
            yield return null;
        }

        // Une fois l'effet terminé, charger la scène suivante
        SceneManager.LoadScene("Open World VR");
    }
}
