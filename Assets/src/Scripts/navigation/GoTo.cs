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

    private Animator _animator;
    private Order _order;
    private int _currentCheckpoint = -1;
    private bool _isMoving;
    private bool _destroyAfterReach;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!_isMoving && _currentCheckpoint < checkpoints.Count - 1)
        {
            _currentCheckpoint++;
            ReachNextCheckpoint();
        }
    }

    public void Sit()
    {
        _animator.SetTrigger("StandToSit");
    }

    public void Stand()
    {
        _animator.SetTrigger("SitToStand");
    }
    
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
        for (int i = checkpoints.Count - 2; i >= 0; i--)
            reversedCheckpoints.Add(checkpoints[i]);

        checkpoints = reversedCheckpoints;
        _currentCheckpoint = -1;
        _destroyAfterReach = true;
    }

    private void ReachNextCheckpoint()
    {
        _isMoving = true;
        Transform checkpoint = checkpoints[_currentCheckpoint];
        Moroutine.Create(ReachCheckpoint(checkpoint)).Run(false);
    }

    private IEnumerator ReachCheckpoint(Transform checkpoint)
    {
        if (_animator.IsInTransition(0))
            yield return new WaitForSeconds(2.5f);
        Vector3 targetPosition = checkpoint.position;
        transform.DOMove(targetPosition, 5f).SetEase(Ease.Linear);
        transform.DOLookAt(targetPosition, 0.5f);
        yield return new WaitForSeconds(5f);
        _isMoving = false;
        if (_currentCheckpoint == checkpoints.Count - 1)
        {
            if (_destroyAfterReach)
                Destroy(gameObject);
            onLastCheckpointReached?.Invoke(this);
        }
    }
}