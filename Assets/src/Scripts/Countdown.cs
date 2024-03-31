using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float time = 10f;
    private float _timeLeft;
    private Coroutine countdownCoroutine;

    public float TimeLeft
    {
        get { return _timeLeft; }
    }

    void Start()
    {
        _timeLeft = time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (countdownCoroutine == null)
            {
                countdownCoroutine = StartCoroutine(StartCountdown());
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
                countdownCoroutine = null;
            }
        }
    }

    IEnumerator StartCountdown()
    {
        while (_timeLeft > 0)
        {
            yield return null;
            _timeLeft -= Time.deltaTime;
        }
    }
}
