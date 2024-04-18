using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float money;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreLostText;
    [SerializeField] private TMP_Text _scoreWinText;
    [SerializeField] private float _expiredTime; // time you lose in seconds if an order is lost
    
    public float GetMoney() => money;
    
    void UpdateScoreText() => _scoreText.text = $"{money:F2}";
    
    void Update()
    {
        UpdateScoreText();
        if (money <= 0)
        {
            Debug.Log("Loser tu es nul");
            _scoreText.gameObject.SetActive(false);
            _scoreLostText.gameObject.SetActive(false);
            _scoreWinText.gameObject.SetActive(false);
        }
    }

    public void DecreaseScore(float val)
    {
        money -= val;
        UpdateScoreText();
        StartCoroutine(PopMoneyLost());
    }

    public void IncreaseScore(float val)
    {
        money += val;
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
}