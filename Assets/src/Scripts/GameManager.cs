using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreLostText;
    [SerializeField] private TMP_Text _scoreWinText;
    [SerializeField] private float _expiredTime; // time you lose in seconds if an order is lost

    private bool _isBlinking;

    void UpdateScoreText() => _scoreText.text = $"{_score:F2}";
    
    void Update()
    {
        UpdateScoreText();
        if (_score <= 0)
        {
            Debug.Log("Loser tu es nul");
            _scoreText.gameObject.SetActive(false);
            _scoreLostText.gameObject.SetActive(false);
            _scoreWinText.gameObject.SetActive(false);
        }
    }

    public void DecreaseScore(float val)
    {
        _score -= val;
        UpdateScoreText();
        StartCoroutine(PopMoneyLost());
    }

    public void IncreaseScore(float val)
    {
        _score += val;
        UpdateScoreText();
        StartCoroutine(PopMoneyWin());
    }
    IEnumerator PopMoneyLost()
    {
        _scoreLostText.text = $"- {_expiredTime}";
        _scoreLostText.gameObject.SetActive(true);
        _scoreLostText.transform.DOScale(1.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _scoreLostText.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _scoreLostText.gameObject.SetActive(false);
    }
    
    IEnumerator PopMoneyWin()
    {
        _scoreWinText.text = $"+ {_expiredTime}";
        _scoreWinText.gameObject.SetActive(true);
        _scoreWinText.transform.DOScale(1.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _scoreWinText.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _scoreWinText.gameObject.SetActive(false);
    }
    
    IEnumerator BlinkTimerText()
    {
        _isBlinking = true;
        while (true)
        {
            Vector3 originalScale = _scoreText.transform.localScale;
            Vector3 largeScale = originalScale * 1.25f;
            Vector3 originalPosition = _scoreText.rectTransform.anchoredPosition;
            float offset = (_scoreText.rectTransform.sizeDelta.x / 2) * (largeScale.x - originalScale.x);

            _scoreText.color = Color.red;
            _scoreText.transform.DOScale(largeScale, 0.5f).SetLoops(2, LoopType.Yoyo);
            _scoreText.rectTransform.DOAnchorPosX(originalPosition.x + offset, 0.5f).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(1f);
            _scoreText.color = Color.white;
            _scoreText.transform.DOScale(originalScale, 0.5f);
            _scoreText.rectTransform.DOAnchorPosX(originalPosition.x, 0.5f);
        }
    }
}