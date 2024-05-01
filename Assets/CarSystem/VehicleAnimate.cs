using UnityEngine;

public class VehicleAnimate : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 50f;
    Quaternion targetRotation = Quaternion.identity;
    private bool isRotationComplete = true;
    bool stop;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (!stop)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.rotation != targetRotation)
        {
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = newRotation;
            isRotationComplete = false;
        }else{
            
            isRotationComplete = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("VehicleDirectionBlock") && isRotationComplete)
        {
            VehicleDirectionTrigger vehicleDirectionTrigger = other.gameObject.GetComponent<VehicleDirectionTrigger>();
            if (vehicleDirectionTrigger == null) return;
            Direction directionToGo = vehicleDirectionTrigger.GetDirection();

            if (directionToGo == Direction.Right)
            {
                targetRotation = Quaternion.LookRotation(transform.right);
            }
            else if (directionToGo == Direction.Left)
            {
                targetRotation = Quaternion.LookRotation(-transform.right);
            }else if(directionToGo == Direction.Stop){
                stop = true;
            }
        }
    }
}
