using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;

public class GoTo : MonoBehaviour
{
    [SerializeField] private List<Transform> checkpoints;

    private int _currentCheckpoint = -1;
    private bool _isMoving;

    public void AddCheckpoint(Transform checkpoint)
    {
        checkpoints.Add(checkpoint);
    }
    
    void Update()
    {
        if (!_isMoving && _currentCheckpoint < checkpoints.Count - 1)
        {
            _currentCheckpoint++;
            ReachNextCheckpoint();
        }
    }

    private void ReachNextCheckpoint()
    {
        _isMoving = true;
        StartCoroutine(ReachCheckpoint(checkpoints[_currentCheckpoint]));
    }

    private IEnumerator ReachCheckpoint(Transform checkpoint)
    {
        transform.DOMove(checkpoint.position, 5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(5f);
        _isMoving = false;
    }
}