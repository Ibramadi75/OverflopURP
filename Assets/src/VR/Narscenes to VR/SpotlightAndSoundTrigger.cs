using UnityEngine;

public class SpotlightAndSoundTrigger : MonoBehaviour
{
    public Light spotlight1;            // Spotlight à activer
    public Light spotlight2;            // Spotlight à activer
    public Light spotlight3;            // Spotlight à activer
    public Light spotlight4;            // Spotlight à activer
    public AudioSource audioSource;    // Audio à jouer
    public float delayInSeconds = 2f;  // Délai avant déclenchement
    public SceneLoader sceneLoader; // Référence au SceneLoader pour charger la scène
    public Persistent persistent; // Référence à l'objet Persistent pour la gestion de l'état

    void Start()
    {
        // On désactive le spotlight au départ
        if (spotlight1 != null && persistent.day < 2)
            spotlight1.enabled = false;

        if (spotlight2 != null && persistent.day < 3)
            spotlight2.enabled = false;
            
        if (spotlight3 != null && persistent.day < 4)
            spotlight3.enabled = false;

        if (spotlight4 != null && persistent.day < 5)
            spotlight4.enabled = false;
        // On lance la coroutine
        StartCoroutine(ActivateAfterDelay());
    }

    private System.Collections.IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (persistent.day > 0)
        {
            spotlight1.enabled = true;
        }

        if (persistent.day > 1)
        {
            spotlight2.enabled = true;
        }

        if (persistent.day > 2)
        {
            spotlight3.enabled = true;
        }

        if (persistent.day > 3)
        {
            spotlight4.enabled = true;
        }

        Debug.Log("Spotlights activated");

        if (audioSource != null)
            audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        // Charge la scène après que le son a fini de jouer
        if (sceneLoader != null)
        {
            sceneLoader.ChangeScene("Restau VR");
        }
        else
        {
            Debug.LogWarning("SceneLoader is not assigned in SpotlightAndSoundTrigger.");
        }

    }
}
