using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sensitivity = 10.0f;
    private bool _isInUi;

    private float _rotationX;

    public Camera PlayerCam { get; private set; }

    void Start()
    {
        PlayerCam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (_isInUi) return;

        var mouseX = Input.GetAxis("Mouse X") * sensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        PlayerCam.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var camRotation = PlayerCam.transform.rotation;
        var forwardDirection = camRotation * Vector3.forward;
        var rightDirection = camRotation * Vector3.right;

        // Ignore Y-axis movement
        forwardDirection.y = 0;
        rightDirection.y = 0;

        var movement = (forwardDirection * vertical / 100 + rightDirection * horizontal).normalized;
        transform.position += movement * speed * Time.deltaTime;

        var direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                              PlayerCam.transform.eulerAngles.y;
            var moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDirection.normalized * speed * Time.deltaTime;
        }
    }

    public void SetIsInUi(bool isInUi)
    {
        _isInUi = isInUi;
    }
}