using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private TMP_Text _timerText;

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

    void UpdateTimer()
    {
        _timer -= Time.deltaTime;
        _timerText.text = $"Temps restant: {_timer:F2}";
    }
}