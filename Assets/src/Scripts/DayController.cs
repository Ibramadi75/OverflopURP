using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayController : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    
    private TMP_Text _dayText;
    private Vignette _vignette;
    private VRPlayer _playerController;
    private CloudsToy _cloudsToy;
    
    void Start()
    {
        _cloudsToy = FindObjectOfType<CloudsToy>();
        
        _playerController = FindObjectOfType<VRPlayer>();

        _dayText = GetComponent<TMP_Text>();

        var day = _playerController.Persistent.day;
        
        postProcessVolume.profile.TryGet(out _vignette);

        _dayText.text = $"Jour {day}";
        
        if (_playerController.Rain != null) {
            var main = _playerController.Rain.main;
            main.startColor = new Color(1f, 1f, 1f, 0.33f * day);
            main.startSpeed = 5 * day;
            
            var emission = _playerController.Rain.emission;
            emission.rateOverTime = 3000 * day;
        
            var velocity = _playerController.Rain.velocityOverLifetime;
            velocity.speedModifier = 1 * day;
        }

        if (_cloudsToy == null) return;
        _cloudsToy.NumberClouds = 50 * day;
        _cloudsToy.VelocityMultipier = 2 * day;
    }
}
