using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnContact : MonoBehaviour
{
    public Material[] newMaterials; // Liste de nouveaux matériaux
    private Renderer meshRenderer; // Référence au composant Mesh Renderer
    public Transform teleportTarget;
    public string teleportTag;

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && other.gameObject.CompareTag(teleportTag))
        {
            other.gameObject.transform.position = teleportTarget.position;
            // Changer l'ordre des matériaux du Mesh Renderer
            if (meshRenderer != null && newMaterials != null && newMaterials.Length > 0)
            {
                // Réorganiser les matériaux dans le tableau
                Material[] currentMaterials = meshRenderer.materials;
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    currentMaterials[i] = newMaterials[i % newMaterials.Length];
                }

                // Appliquer les nouveaux matériaux
                meshRenderer.materials = currentMaterials;
            }
        }
    }
}
