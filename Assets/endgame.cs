using System.Collections;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshPro endText; // Le texte à faire apparaître
    public CanvasGroup fadeOverlay; // Le CanvasGroup pour assombrir l'écran

    void Start()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        // Fade-in du texte (TextMeshPro)
        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
            endText.alpha = t;
            yield return null;
        }

        yield return new WaitForSeconds(5);

        // Assombrir l'écran
        for (float t = 0; t <= 1; t += Time.deltaTime * 0.5f) // ralentir le fondu (ajuster le facteur multiplicatif)
        {
            fadeOverlay.alpha = t;
            yield return null;
        }

        // Attendre encore un peu après le fondu
        yield return new WaitForSeconds(1);

        // Quitter le jeu
        Application.Quit();

        // Si dans l'éditeur, pour tester
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
