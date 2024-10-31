using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // Lumière directionnelle principale (pour le soleil/lune)
    public Color dayColor = Color.white; // Couleur de jour
    public Color nightColor = Color.blue; // Couleur de nuit
    public float dayIntensity = 1.0f; // Intensité de jour
    public float nightIntensity = 0.1f; // Intensité de nuit
    public float transitionDuration = 2.0f; // Durée de la transition

    private bool isDay = true;
    private float transitionProgress = 0.0f;

    private void Update()
    {
        // Si la transition est en cours
        if (transitionProgress < 1.0f)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            UpdateLighting();
        }
    }

    // Fonction pour déclencher la transition jour-nuit
    public void ToggleDayNight()
    {
        isDay = !isDay;
        transitionProgress = 0.0f;
    }

    // Fonction pour ajuster la lumière selon le moment de la journée
    private void UpdateLighting()
    {
        Color targetColor = isDay ? dayColor : nightColor;
        float targetIntensity = isDay ? dayIntensity : nightIntensity;

        directionalLight.color = Color.Lerp(directionalLight.color, targetColor, transitionProgress);
        directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, targetIntensity, transitionProgress);

        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetColor, transitionProgress);
    }
}
