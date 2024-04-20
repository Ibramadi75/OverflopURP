using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private PlayerController _playerController;
    private GameObject _lastHitObject;
    private Color _originalColor;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    void Update() {
        if (Physics.Raycast(_playerController.PlayerCam.transform.position, _playerController.PlayerCam.transform.forward, out RaycastHit hit, 2, 1))
        {
            AbstractInteraction abstractInteraction = hit.transform.GetComponent<AbstractInteraction>();
            
            if (abstractInteraction is null)
                abstractInteraction = hit.transform.GetComponentInChildren<AbstractInteraction>();
                
            if (abstractInteraction)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    abstractInteraction.MainInteraction(gameObject);
                    
                if (Input.GetKey(KeyCode.F))
                    abstractInteraction.SecondaryInteraction(gameObject);
            }

            if (_lastHitObject != hit.transform.gameObject)
            {
                var canInteractWith = hit.transform.gameObject.GetComponent<CanInteractWith>();
                if (canInteractWith is not null)
                {
                    canInteractWith.isInteracting = true;
                    if (_lastHitObject != null)
                        RestoreOriginalColor(_lastHitObject);

                    _lastHitObject = hit.transform.gameObject;

                    if (_originalColor != hit.transform.GetComponent<Renderer>().material.color)
                        _originalColor = hit.transform.GetComponent<Renderer>().material.color;

                    ChangeColorToBlack(hit.transform.gameObject);
                }
            }
        }
        else
        {
            if (_lastHitObject != null)
            {
                RestoreOriginalColor(_lastHitObject);
                _lastHitObject = null;
            }
        }
    }
    
    void ChangeColorToBlack(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer is null)
            renderer = obj.GetComponentInChildren<Renderer>();

        if (renderer != null)
        {
            Material[] materials = renderer.materials;
            foreach (Material material in materials)
            {
                material.color = new Color (0f, 0f, 1f, .4f);
            }
        }
    }

    void RestoreOriginalColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer is null)
            renderer = obj.GetComponentInChildren<Renderer>();

        if (renderer != null)
        {
            Material[] materials = renderer.materials;
            foreach (Material material in materials)
            {
                material.color = _originalColor;
            }
        }
    }
}