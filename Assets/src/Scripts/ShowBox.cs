using UnityEngine;

public class ShowBox : MonoBehaviour
{
    public Mesh newMesh;

    private void Start()
    {
        var childFilters = GetComponentsInChildren<MeshFilter>();

        foreach (var filter in childFilters) filter.mesh = newMesh;
    }
}