using System;
using UnityEngine;

public class Jauge : MonoBehaviour
{
    private Countdown _countdownScript;
    private float _totalTime;

    private float _xDefaultLocalScale;
    private float _xDefaultLocalPosition;

    void Start()
    {
        _countdownScript = GetComponentInParent<Countdown>();

        if (_countdownScript is null)
        {
            Debug.Log("A countdown script must be attached to the parent.");
        }
        else
        {
            _totalTime = _countdownScript.time;
            _xDefaultLocalScale = transform.localScale.x;
            _xDefaultLocalPosition = transform.localPosition.x;
        }
    }

    void Update()
    {
        float remainingTime = _countdownScript.TimeLeft;
        Vector3 localScale = transform.localScale;
        Vector3 localPosition = transform.localPosition;

        float xLocalScale = remainingTime / _totalTime + _xDefaultLocalScale;
        float xLocalPosition = (remainingTime / _totalTime) / 2 + _xDefaultLocalPosition;

        if (xLocalScale >= 0)
        {
            transform.localScale = new Vector3(xLocalScale, localScale.y, localScale.z);
        }
        
        transform.localPosition = new Vector3(xLocalPosition, localPosition.y, localPosition.z);
    }
}
