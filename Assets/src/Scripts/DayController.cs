using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayController : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    
    private TMP_Text _dayText;
    private Vignette _vignette;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _dayText = GetComponent<TMP_Text>();

        var day = _playerController.Persitent.day;
        
        postProcessVolume.profile.TryGet(out _vignette);

        _dayText.text = $"Jour {day}";
        _vignette.color.value = day == 1 ? Color.black : Color.red;
        _vignette.intensity.value = 0.07f * day;

        var main = _playerController.Rain.main;
        main.startColor = new Color(1f, 1f, 1f, 0.33f * day);
        
        var emission = _playerController.Rain.emission;
        emission.rateOverTime = 3000 * day;
        
        var velocity = _playerController.Rain.velocityOverLifetime;
        velocity.speedModifier = 1 * day;
    }
}
