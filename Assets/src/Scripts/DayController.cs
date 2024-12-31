using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayController : MonoBehaviour
{
    private TMP_Text _dayText;
    private int _day;
    private Vignette _vignette;

    public int Day => _day;
    
    public void NextDay()
    {
        _day++;
        _dayText.text = $"Jour {_day}";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _dayText = GetComponent<TMP_Text>();
        _day = 1;
    }

    private void UpdateVignette()
    {
        _vignette.color = _day == 1 ? (ColorParameter)Color.black : (ColorParameter)Color.red;
        _vignette.intensity = 0.15f * _day;
    }
}
