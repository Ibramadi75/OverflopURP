using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // Référence à l'AudioSource de la musique
    [SerializeField] private float bobbingSpeed = 0.2f;  // Vitesse de l'oscillation
    [SerializeField] private float bobbingAmount = 0.1f; // Amplitude du mouvement (hauteur du hochement de tête)
    [SerializeField] private float restPositionY = 0f;  // Position Y de repos de la caméra (par défaut 0)

    private Vector3 originalPosition;

    void Start()
    {
        // Sauvegarder la position d'origine de la caméra
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            // Calculez un mouvement de hochement de tête basé sur un sinus pour simuler le rythme
            float yOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + yOffset, originalPosition.z);
        }
        else
        {
            // Si la musique ne joue pas, la caméra revient à sa position d'origine
            transform.localPosition = originalPosition;
        }
    }
}
