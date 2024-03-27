using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float time = 10f;
    private float _timeLeft;
    private Coroutine countdownCoroutine;
    [SerializeField] bool byInteraction = false;
    [SerializeField] bool launchCoroutine = false;

    public float TimeLeft
    {
        get { return _timeLeft; }
    }

    void Start()
    {
        _timeLeft = time;

        if (!byInteraction)
            StartCoroutine(StartCountdown());
    }

    void Update()
    {
        launchCoroutine = false;
        if (Input.GetKey(KeyCode.Space))
            launchCoroutine = true;

        if (byInteraction)
        {
            if (launchCoroutine)
            {
                if (countdownCoroutine == null)
                    countdownCoroutine = StartCoroutine(StartCountdown());
            }
            else
            {
                if (countdownCoroutine != null)
                {
                    StopCoroutine(countdownCoroutine);
                    countdownCoroutine = null;
                    launchCoroutine = false;
                }
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
