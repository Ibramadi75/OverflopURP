using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private float _time;
    private float _timeLeft;

    public float TimeLeft
    {
        get { return _timeLeft; }
    }

    public void SetTime(float time) => _time = time;
    public float GetTime() => _time;

    void Start()
    {
        _timeLeft = _time;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (_timeLeft > 0)
        {
            yield return null;
            _timeLeft -= Time.deltaTime;
        }
    }

    void Update()
    {
    }
}
