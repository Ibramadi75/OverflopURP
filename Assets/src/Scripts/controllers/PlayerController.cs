using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sensitivity = 10.0f;

    private Camera _playerCam;
    private float _rotationX = 0.0f;

    public Camera PlayerCam => _playerCam;

    void Start()
    {
        _playerCam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        _playerCam.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Quaternion camRotation = _playerCam.transform.rotation;
        Vector3 forwardDirection = camRotation * Vector3.forward;
        Vector3 rightDirection = camRotation * Vector3.right;

        // Ignore Y-axis movement
        forwardDirection.y = 0;
        rightDirection.y = 0;

        Vector3 movement = (forwardDirection * vertical / 100 + rightDirection * horizontal).normalized;
        transform.position += movement * speed * Time.deltaTime;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                _playerCam.transform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDirection.normalized * speed * Time.deltaTime;
        }
    }
}