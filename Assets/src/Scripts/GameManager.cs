using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _timeLostText;
    [SerializeField] private float _expiredTime; // time you lose in seconds if an order is lost

    private bool _isBlinking;

    void UpdateTimerText() => _timerText.text = $"Temps restant: {_timer:F2}";
    
    void Update()
    {
        _timer -= Time.deltaTime;
        UpdateTimerText();
        
        if (_timer <= 0)
        {
            Debug.Log("Loser tu es nul");
            Time.timeScale = 0;
            _timerText.gameObject.SetActive(false);
            _timeLostText.gameObject.SetActive(false);
        }
        
        else if (!_isBlinking && _timer <= 20)
            StartCoroutine(BlinkTimerText());
        
    }

    public void RemoveTime()
    {
        _timer -= _expiredTime;
        UpdateTimerText();
        StartCoroutine(PopTimeLost());
    }

    IEnumerator PopTimeLost()
    {
        _timeLostText.gameObject.SetActive(true);
        _timeLostText.transform.DOScale(1.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _timeLostText.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _timeLostText.gameObject.SetActive(false);
    }
    
    IEnumerator BlinkTimerText()
    {
        _isBlinking = true;
        while (true)
        {
            Vector3 originalScale = _timerText.transform.localScale;
            Vector3 largeScale = originalScale * 1.25f;
            Vector3 originalPosition = _timerText.rectTransform.anchoredPosition;
            float offset = (_timerText.rectTransform.sizeDelta.x / 2) * (largeScale.x - originalScale.x);

            _timerText.color = Color.red;
            _timerText.transform.DOScale(largeScale, 0.5f).SetLoops(2, LoopType.Yoyo);
            _timerText.rectTransform.DOAnchorPosX(originalPosition.x + offset, 0.5f).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(1f);
            _timerText.color = Color.white;
            _timerText.transform.DOScale(originalScale, 0.5f);
            _timerText.rectTransform.DOAnchorPosX(originalPosition.x, 0.5f);
        }
    }
}