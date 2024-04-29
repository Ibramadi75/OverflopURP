using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public string tagToDestroy; // Tag de l'objet à détruire

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si la collision est avec un autre GameObject
        if (other.gameObject.CompareTag(tagToDestroy))
        {
            // Détruit le GameObject actuel
            Destroy(gameObject);
        }
    }
}
