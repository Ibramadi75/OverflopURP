using UnityEngine;

public class Jauge : MonoBehaviour
{
    [SerializeField] Countdown countdownScript;
    private float _totalTime;

    void Start()
    {
        _totalTime = countdownScript.time;
    }

    void Update()
    {
        float remainingTime = countdownScript.TimeLeft;
        transform.localScale = new Vector3(remainingTime / _totalTime, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3((remainingTime / _totalTime)/2 -.5f, transform.localPosition.y, transform.localPosition.z);
    }
}
