using UnityEngine;
using UnityEditor;

public enum FloatingPosition
{
    Top,
    Bottom,
    Left,
    Right
}

public class FloatingPrefab : MonoBehaviour
{
    [SerializeField] GameObject prefabToInstantiate;
    [SerializeField] bool autoOffset;
    [SerializeField] float offset;
    [SerializeField] FloatingPosition position;

    void Start()
    {

    }


    private Vector3 GetPositionVector(FloatingPosition pos)
    {
        switch (pos)
        {
            case FloatingPosition.Bottom:
                return Vector3.down + new Vector3(0, -offset, 0);
            case FloatingPosition.Left:
                return Vector3.left + new Vector3(-offset, 0, 0);
            case FloatingPosition.Right:
                return Vector3.right + new Vector3(offset, 0, 0);
            default:
                return Vector3.up + new Vector3(0, offset, 0);
        }
    }
}