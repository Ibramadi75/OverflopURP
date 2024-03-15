using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sensivity;
    
    private Camera camera;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY * sensivity;
        rotationY += mouseX * sensivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        
        camera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Quaternion cameraRotation = camera.transform.rotation;
        Vector3 forwardDirection = cameraRotation * Vector3.forward;
        Vector3 rightDirection = cameraRotation * Vector3.right;
        Vector3 movement = (forwardDirection * vertical + rightDirection * horizontal).normalized;
    
        transform.position += movement * speed * Time.deltaTime;
    }
}
