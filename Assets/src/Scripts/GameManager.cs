using System;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private TMP_Text _timerText;
    [ReadOnly] [SerializeField] private string _description = "Expired Time is the amount of time you lose in seconds";
    [SerializeField] private float _expiredTime;

    void Update()
    {
        UpdateTimer();
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
        _timerText.text = $"Temps restant: {_timer:F2}";
    }

    void UpdateTimer()
    {
        _timer -= Time.deltaTime;
        _timerText.text = $"Temps restant: {_timer:F2}";
    }
}