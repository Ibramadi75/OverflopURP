using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Redcode.Moroutines;
using UnityEngine;

public class GoTo : MonoBehaviour
{
    public delegate void OnLastCheckpointReached(GoTo npc);

    public OnLastCheckpointReached onLastCheckpointReached;
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<Transform> checkpoints;
    
    private Order _order;
    private int _currentCheckpoint = -1;
    private bool _isMoving;
    private bool _destroyAfterReach;
    
    public Vector3 GetSpawnPoint()
    {
        return spawnPoint.position;
    }

    public void SetOrder(Order order)
    {
        _order = order;
    }

    public Order GetOrder()
    {
        return _order;
    }
    
    public void AddCheckpoint(Transform checkpoint)
    {
        checkpoints.Add(checkpoint);
    }
    
    public void ReverseCheckpointsOrder()
    {
        List<Transform> reversedCheckpoints = new List<Transform>();
        for (int i = checkpoints.Count - 1; i >= 0; i--)
            reversedCheckpoints.Add(checkpoints[i]);

        checkpoints = reversedCheckpoints;
        _currentCheckpoint = -1;
        _destroyAfterReach = true;
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
        Transform checkpoint = checkpoints[_currentCheckpoint];
        Moroutine.Create(ReachCheckpoint(checkpoint)).Run(false);
    }

    private IEnumerator ReachCheckpoint(Transform checkpoint)
    {
        transform.DOMove(checkpoint.position, 1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        _isMoving = false;
        if (_currentCheckpoint == checkpoints.Count - 1)
        {
            if (_destroyAfterReach)
                Destroy(gameObject);
            onLastCheckpointReached?.Invoke(this);
        }
    }
}