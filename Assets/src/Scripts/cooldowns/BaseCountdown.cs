using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

public class BaseCountdown : MonoBehaviour
{
    protected GameObject countdownPrefab;
    
    private float _baseTime;
    private Moroutine _countdownMoroutine;
    private Moroutine _reduceSizeMoroutine;
    private float _remainingTime;
    private float _xDefaultLocalScale;
    private float _xDefaultLocalPosition;
    
    void Awake()
    {
        _remainingTime = _baseTime;
        _countdownMoroutine = Moroutine.Create(Countdown());
        _reduceSizeMoroutine = Moroutine.Create(ReduceSize());
    }
    
    protected void StartMoroutines()
    {
        _countdownMoroutine.Run(false);
        _reduceSizeMoroutine.Run(false);
    }

    protected void StopMoroutines()
    {
        _countdownMoroutine.Stop();
        _reduceSizeMoroutine.Stop();
    }

    protected void RespawnCountdown()
    {
        Instantiate(countdownPrefab, transform.parent);
        Destroy(gameObject);
    }

    protected bool AreMoroutinesCompleted() => _countdownMoroutine.IsCompleted && _reduceSizeMoroutine.IsCompleted;
    
    private IEnumerator Countdown()
    {
        while (_remainingTime > 0)
        {
            yield return null;
            _remainingTime -= Time.deltaTime;
        }
    }

    private IEnumerator ReduceSize()
    {
        while (_remainingTime > 0)
        {
            yield return null;
            // Calculer la nouvelle échelle en fonction du temps restant
            float xLocalScale = Mathf.Clamp((_remainingTime / _baseTime) * _xDefaultLocalScale, 0f, _xDefaultLocalScale);

            // Calculer la nouvelle position en fonction du temps restant
            float xLocalPosition = Mathf.Clamp((_xDefaultLocalPosition - (_xDefaultLocalScale - xLocalScale) / 2f),
                _xDefaultLocalPosition - _xDefaultLocalScale / 2f,
                _xDefaultLocalPosition + _xDefaultLocalScale / 2f);

            // Mettre à jour la taille et la position du gameObject
            transform.localScale = new Vector3(xLocalScale, transform.localScale.y, transform.localScale.z);
            transform.localPosition = new Vector3(xLocalPosition, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void SetBaseTime(float baseTime) => _baseTime = baseTime;
}