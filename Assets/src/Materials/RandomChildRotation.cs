using UnityEngine;

public class RandomChildRotation : MonoBehaviour
{
    public float minPeakSpeed = 1000f;
    public float maxPeakSpeed = 3000f;
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<RotateOnXAxis>() == null)
                {
                    float peakSpeed = Random.Range(minPeakSpeed, maxPeakSpeed);
                    child.gameObject.AddComponent<RotateOnXAxis>().SetRotationSpeed(peakSpeed, accelerationTime, decelerationTime);
                }
            }
        }
    }
}

public class RotateOnXAxis : MonoBehaviour
{
    private float peakSpeed;
    private float currentSpeed;
    private float accelerationTime;
    private float decelerationTime;
    private bool isAccelerating = true;
    private float timeElapsed = 0f;

    public void SetRotationSpeed(float peakSpeed, float accelerationTime, float decelerationTime)
    {
        this.peakSpeed = peakSpeed;
        this.accelerationTime = accelerationTime;
        this.decelerationTime = decelerationTime;
        currentSpeed = 0f;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (isAccelerating)
        {
            currentSpeed = Mathf.Lerp(0, peakSpeed, timeElapsed / accelerationTime);
            if (timeElapsed >= accelerationTime)
            {
                isAccelerating = false;
                timeElapsed = 0f;
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(peakSpeed, 0, timeElapsed / decelerationTime);
        }

        transform.Rotate(Vector3.down * currentSpeed * Time.deltaTime);
    }
}