using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRRadio : MonoBehaviour
{
    private XRSimpleInteractable _xrSimpleInteractable;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
        _xrSimpleInteractable.selectEntered.AddListener(Selected);
    }

    void Selected(SelectEnterEventArgs args)
    {
        _audioSource.mute = !_audioSource.mute;
    }
    
    void OnDisable()
    {
        _xrSimpleInteractable.selectEntered.RemoveListener(Selected);
    }
}
