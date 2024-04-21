using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

                transform.rotation = rotation;
            }
        }
    }
}
