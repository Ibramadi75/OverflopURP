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

        postProcessVolume.profile.TryGet(out _vignette);

        _dayText.text = $"Jour {_playerController.Persitent.day}";
        _vignette.color.value = _playerController.Persitent.day == 1 ? Color.black : Color.red;
        _vignette.intensity.value = 0.15f * _playerController.Persitent.day;
    }
}
