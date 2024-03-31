using UnityEngine;

public class Jauge : MonoBehaviour
{
    public Countdown _countdownScript;
    private float _totalTime;
    private float _xDefaultLocalScale;
    private float _xDefaultLocalPosition;

    void Start()
    {
        if (_countdownScript is not null)
        {
            _totalTime = _countdownScript.GetTime();
            _xDefaultLocalScale = transform.localScale.x;
            _xDefaultLocalPosition = transform.localPosition.x;
        }
    }

    void Update()
    {
        if (_countdownScript is not null)
        {
            float remainingTime = _countdownScript.TimeLeft;

            // Calculer la nouvelle échelle en fonction du temps restant
            float xLocalScale = Mathf.Clamp((remainingTime / _totalTime) * _xDefaultLocalScale, 0f, _xDefaultLocalScale);

            // Calculer la nouvelle position en fonction du temps restant
            float xLocalPosition = Mathf.Clamp((_xDefaultLocalPosition - (_xDefaultLocalScale - xLocalScale) / 2f), 
                                                _xDefaultLocalPosition - _xDefaultLocalScale / 2f, 
                                                _xDefaultLocalPosition + _xDefaultLocalScale / 2f);

            // Mettre à jour la taille et la position du gameObject
            transform.localScale = new Vector3(xLocalScale, transform.localScale.y, transform.localScale.z);
            transform.localPosition = new Vector3(xLocalPosition, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
