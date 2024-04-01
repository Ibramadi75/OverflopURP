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
        }
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
}