using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Charge une scène Unity par son nom.
    /// </summary>
    /// <param name="sceneName">Nom exact de la scène à charger (doit être ajoutée dans le Build Settings).</param>
    public void ChangeScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Vérifie que la scène existe dans le Build Settings
            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"La scène \"{sceneName}\" n'existe pas dans les Build Settings.");
            }
        }
        else
        {
            Debug.LogWarning("Nom de scène vide ou nul.");
        }
    }
}
