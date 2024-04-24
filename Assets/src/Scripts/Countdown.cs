using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public delegate void OnComplete();

    [SerializeField] private bool isAuto;
    private float _baseTime;
    private Moroutine _countdownMoroutine;

    private bool _isTriggering;
    private Vector3 _originalPosition;
    private Vector3 _originalScale;
    private float _remainingTime;
    private float _xDefaultLocalPosition;
    private float _xDefaultLocalScale;
    public OnComplete onComplete;

    void Awake()
    {
        _xDefaultLocalScale = transform.localScale.x;
        _xDefaultLocalPosition = transform.localPosition.x;
    }

    void Update()
    {
        if (isAuto) return;
        if (_isTriggering)
            ResumeMoroutine();
        _isTriggering = false;
        StopMoroutine();
    }

    private void OnEnable()
    {
        CreateMoroutine();
        if (isAuto) ResumeMoroutine();
    }

    private void OnDisable()
    {
        ResetSize();
    }

    public void SetTime(float baseTime)
    {
        _baseTime = baseTime;
    }

    public void SetIsTriggering(bool isTriggering)
    {
        _isTriggering = isTriggering;
    }

    private void CreateMoroutine()
    {
        _remainingTime = _baseTime;
        _countdownMoroutine = Moroutine.Create(CountdownRunner()).OnCompleted(c =>
        {
            onComplete?.Invoke();
            gameObject.SetActive(false);
        });
    }

    public bool IsFinished()
    {
        return _remainingTime <= 0;
    }

    private void ResumeMoroutine()
    {
        _countdownMoroutine.Run(false);
    }

    private void StopMoroutine()
    {
        _countdownMoroutine.Stop();
    }

    private void ResetSize()
    {
        transform.localPosition =
            new Vector3(_xDefaultLocalPosition, transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(_xDefaultLocalScale, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator CountdownRunner()
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
        var progressRatio = _remainingTime / _baseTime;

        // Limiter le ratio de progression entre 0 et 1
        progressRatio = Mathf.Clamp01(progressRatio);

        // Appliquer le ratio de progression à la taille de l'échelle sur l'axe X
        var newScaleX = Mathf.Lerp(0f, _xDefaultLocalScale, progressRatio);

        // Appliquer la nouvelle taille de l'échelle
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
    }
}