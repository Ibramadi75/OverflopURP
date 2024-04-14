using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBox : MonoBehaviour
{
    public Mesh newMesh;

    void Start()
    {
        MeshFilter[] childFilters = GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter filter in childFilters)
        {
            filter.mesh = newMesh;
        }
    }

    void Update()
    {
        
    }
}
