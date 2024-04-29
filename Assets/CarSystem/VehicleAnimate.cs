using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAnimate : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 50f;
    Quaternion targetRotation = Quaternion.identity;
    private bool isRotationComplete = true;
    bool stop = false;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (!stop)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Si la rotation actuelle n'est pas égale à la rotation cible
        if (transform.rotation != targetRotation)
        {
            // Calculer la nouvelle rotation intermédiaire
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Appliquer la nouvelle rotation
            transform.rotation = newRotation;
            isRotationComplete = false;
        }else{
            
            isRotationComplete = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with " + other.gameObject.name);
        if (other.gameObject.tag == "VehicleDirectionBlock" && isRotationComplete)
        {
            // Obtenir la direction souhaitée à partir du script VehicleDirectionTrigger
            VehicleDirectionTrigger vehicleDirectionTrigger = other.gameObject.GetComponent<VehicleDirectionTrigger>();
            Direction directionToGo = vehicleDirectionTrigger.GetDirection();

            // Définir la rotation vers la droite ou la gauche par rapport à la direction actuelle du GameObject
            if (directionToGo == Direction.Right)
            {
                targetRotation = Quaternion.LookRotation(transform.right); // Rotation de 90 degrés vers la droite
            }
            else if (directionToGo == Direction.Left)
            {
                targetRotation = Quaternion.LookRotation(-transform.right); // Rotation de 90 degrés vers la gauche
            }else if(directionToGo == Direction.Stop){
                stop = true;
            }
        }
    }
}
