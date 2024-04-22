using UnityEngine;

public class CanInteractWith : MonoBehaviour
{
    private Outline _outline;

    void Start()
    {
        _outline = gameObject.AddComponent<Outline>();
        _outline.OutlineColor = Color.red;
        _outline.OutlineWidth = 10f;
        _outline.enabled = false;
    }

    void IsHovered()
    {
        _outline.enabled = true;
    }

    void IsNotHovered()
    {
        _outline.enabled = false;
    }
}