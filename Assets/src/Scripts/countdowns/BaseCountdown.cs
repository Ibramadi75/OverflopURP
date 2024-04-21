using System.Collections;
using DG.Tweening;
using Redcode.Moroutines;
using Unity.Mathematics;
using UnityEngine;

public class BaseCountdown : MonoBehaviour
{
    private Vector3 _originalPosition;
    private Vector3 _originalScale;
    private Moroutine _countdownMoroutine;
    private float _baseTime;
    private float _remainingTime;
    private float _xDefaultLocalScale;
    private float _xDefaultLocalPosition;
    
    public void SetTime(float baseTime) => _baseTime = baseTime;
    public float GetRemainingTime() => _remainingTime;
    
    void Awake()
    {
        _xDefaultLocalScale = transform.localScale.x;
        _xDefaultLocalPosition = transform.localPosition.x;
    }

    protected void CreateMoroutine()
    {
        _remainingTime = _baseTime;
        _countdownMoroutine = Moroutine.Create(Countdown()).OnCompleted(c => gameObject.SetActive(false)).Run(false);
    }
    
    protected void ResumeMoroutine() => _countdownMoroutine.Run(false);

    protected void StopMoroutine() => _countdownMoroutine.Stop();

    protected void ResetSize()
    {
        transform.localPosition =
            new Vector3(_xDefaultLocalPosition, transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(_xDefaultLocalScale, transform.localScale.y, transform.localScale.z);
    }
    
    private IEnumerator Countdown()
    {
        while (_remainingTime > 0)
        {
            yield return null;
            _remainingTime -= Time.deltaTime;
            ReduceSize();
        }
    }

    private void ReduceSize()
    {
        // Calculer le ratio de progression
        float progressRatio = _remainingTime / _baseTime;
    
        // Limiter le ratio de progression entre 0 et 1
        progressRatio = Mathf.Clamp01(progressRatio);
    
        // Appliquer le ratio de progression à la taille de l'échelle sur l'axe X
        float newScaleX = Mathf.Lerp(0f, _xDefaultLocalScale, progressRatio);
    
        // Appliquer la nouvelle taille de l'échelle
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
    }

}