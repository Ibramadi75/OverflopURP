using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float time = 10f;
    private float _timeLeft;

    public float TimeLeft
    {
        get { return _timeLeft; }
    }

    void Start()
    {
        _timeLeft = time;
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
