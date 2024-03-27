using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float time = 10f;
    private float _timeLeft;
    private Coroutine _countdownCoroutine;
    [SerializeField] bool byInteraction = false;
    [SerializeField] bool _launchCoroutine = false;
    bool _primaryInteraction = false;

    public void interactOn() => _primaryInteraction = true;

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
        _launchCoroutine = _primaryInteraction;

        if (byInteraction)
        {
            if (_launchCoroutine)
            {
                if (_countdownCoroutine == null)
                    _countdownCoroutine = StartCoroutine(StartCountdown());
            }
            else
            {
                if (_countdownCoroutine != null)
                {
                    StopCoroutine(_countdownCoroutine);
                    _countdownCoroutine = null;
                    _launchCoroutine = false;
                }
            }
        }

        _launchCoroutine = false;
        _primaryInteraction = false;
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
