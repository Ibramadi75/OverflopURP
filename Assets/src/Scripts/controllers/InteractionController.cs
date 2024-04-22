using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private PlayerController _playerController;
    private GameObject _currentInteractable;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if (Physics.Raycast(_playerController.PlayerCam.transform.position,
                _playerController.PlayerCam.transform.forward, out RaycastHit hit, 2, 1))
        {
            GameObject interactable = hit.transform.gameObject;
            AbstractInteraction abstractInteraction = hit.transform.GetComponent<AbstractInteraction>();

            if (abstractInteraction == null)
                abstractInteraction = hit.transform.GetComponentInChildren<AbstractInteraction>();

            if (interactable != _currentInteractable)
            {
                if (_currentInteractable != null)
                    _currentInteractable.SendMessage("IsNotHovered", SendMessageOptions.DontRequireReceiver);

                if (interactable != null)
                    interactable.SendMessage("IsHovered", SendMessageOptions.DontRequireReceiver);

                _currentInteractable = interactable;
            }

            if (abstractInteraction != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    abstractInteraction.MainInteraction(gameObject);

                if (Input.GetKey(KeyCode.F))
                    abstractInteraction.SecondaryInteraction();
            }
        }

        else if (_currentInteractable != null)
        {
            _currentInteractable.SendMessage("IsNotHovered", SendMessageOptions.DontRequireReceiver);
            _currentInteractable = null;
        }
    }
}