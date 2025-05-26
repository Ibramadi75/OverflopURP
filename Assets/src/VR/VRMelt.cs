using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMelt : MonoBehaviour
{
    [SerializeField] private Countdown countdown;
    
    private AudioSource _audioSource;
    private Ingredient _ingredient;
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        countdown.onComplete += OnCountdownComplete;
        countdown.whileRunning += WhileCountdownRunning;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;
        _ingredient = other.GetComponent<Ingredient>();
        countdown.SetTime(_ingredient.ingredientData.time);
        countdown.gameObject.SetActive(true);
    }
    
    void OnCountdownComplete()
    {
        Instantiate(_ingredient.ingredientData.meltedPrefab, transform.position + new Vector3(0.0f, 0.5f), Quaternion.identity);
        Destroy(_ingredient.gameObject);
        if (!_audioSource.isPlaying) return;
        _audioSource.Stop();
    }

    void WhileCountdownRunning()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}
