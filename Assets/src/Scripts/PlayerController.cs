<using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sensivity = 10.0f;
    
    private Camera playerCam;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
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
        
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Quaternion camRotation = playerCam.transform.rotation;
        Vector3 forwardDirection = camRotation * Vector3.forward;
        Vector3 rightDirection = camRotation * Vector3.right;
        
        // Ignore Y-axis movement
        forwardDirection.y = 0;
        rightDirection.y = 0;
        
        Vector3 movement = (forwardDirection * vertical / 100 + rightDirection * horizontal).normalized;
    
        transform.position += movement * speed * Time.deltaTime;
    }
}
