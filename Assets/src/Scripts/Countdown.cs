using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float _time = 10f;
    private float _timeLeft;
    private Coroutine _countdownCoroutine;
    [SerializeField] bool byInteraction = false;
    [SerializeField] bool _launchCoroutine = false;
    bool _primaryInteraction = false;

    public void InteractOn() => _primaryInteraction = true;
    public void SetTime(float time) => _time = time;
    public float GetTime() => _time;

    public float TimeLeft => _timeLeft;

    void Start()
    {
        if (GetComponent<Ingredient>() is not null)
        {
            _time = GetComponent<Ingredient>().ingredientData.time;
            
        } else if (GetComponent<Recipe>() is not null)
            _time = GetComponent<Recipe>().recipeData.baseExpiration;

        _timeLeft = _time;

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

        if (_timeLeft <= 0)
        {
            gameObject.SetActive(false);
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
