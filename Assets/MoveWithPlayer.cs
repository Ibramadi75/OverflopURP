using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour
{
    public PlayerController playerController; // Référence au PlayerController
    private float lastPlayerX;                // Dernière position X du joueur
    private float initialStructureX;         // Position initiale de la structure
    private const float MaxX = 111f;         // Limite de déplacement en X pour la structure

    void Start()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController non trouvé dans la scène.");
            return;
        }

        // Enregistrer la position initiale
        lastPlayerX = playerController.transform.position.x;
        initialStructureX = transform.position.x;
    }

    void Update()
    {
        if (playerController == null) return;

        // Obtenir la position X actuelle du joueur
        float currentPlayerX = playerController.transform.position.x;

        // Calculer la distance parcourue en X (seulement si le joueur avance)
        float deltaX = currentPlayerX - lastPlayerX;

        if (deltaX > 0)
        {
            // Ajouter le déplacement à la position actuelle de la structure
            float newStructureX = transform.position.x + deltaX;

            // Limiter la position X à la valeur maximale
            newStructureX = Mathf.Min(newStructureX, MaxX);

            // Mettre à jour la position de la structure
            transform.position = new Vector3(newStructureX, transform.position.y, transform.position.z);
        }

        // Mettre à jour la dernière position X du joueur
        lastPlayerX = currentPlayerX;
    }
}
