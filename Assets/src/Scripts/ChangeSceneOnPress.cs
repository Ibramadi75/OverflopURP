using UnityEngine;
using TMPro; // Import pour TextMeshPro
using UnityEngine.SceneManagement;
using System;

public class ChangeSceneOnPressTMP : MonoBehaviour
{
    public string sceneName; // Nom de la scène à charger
    public TMP_Text pressEText;  // Référence au TMP_Text affichant "Press E"

    private bool isPlayerInRange = false;

    void Start()
    {
        // Assure-toi que le texte est désactivé au début
        if (pressEText != null)
        {
            pressEText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si c'est le joueur qui entre en collision
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (pressEText != null)
            {
                pressEText.gameObject.SetActive(true); // Affiche le texte "Press E"
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Cache le texte quand le joueur sort de la zone
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (pressEText != null)
            {
                pressEText.gameObject.SetActive(false); // Cache le texte "Press E"
            }
        }
    }

    void Update()
    {
        // Change de scène si le joueur appuie sur "E" et est dans la zone
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName); // Change la scène
            }
        }

        Debug.Log(isPlayerInRange ? "Player is in range" : "Player is not in range");
    }
}
