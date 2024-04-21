using UnityEngine;

public class DisapearInGame : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void Update()
    {
    }
}