using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sensitivity = 10.0f;
    private Color originalColor;
    private GameObject lastHitObject;

    private Camera playerCam;
    private float rotationX = 0.0f;
    private GameObject[] interactionObjects;

    void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

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

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.transform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDirection.normalized * speed * Time.deltaTime;
        }

        Interact();
    }

    void Interact()
    {
        if (debug)
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 3, Color.green);

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, 2, 1))
        {
            AbstractInteraction abstractInteraction = hit.transform.GetComponent<AbstractInteraction>();

            if (abstractInteraction)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    abstractInteraction.MainInteraction(gameObject);
                    
                if (Input.GetKey(KeyCode.F))
                {
                    abstractInteraction.SecondaryInteraction(gameObject);
                }
            }

            if (lastHitObject != hit.transform.gameObject)
            {
                if (lastHitObject != null)
                    RestoreOriginalColor(lastHitObject);

                lastHitObject = hit.transform.gameObject;

                if (originalColor != hit.transform.GetComponent<Renderer>().material.color)
                    originalColor = hit.transform.GetComponent<Renderer>().material.color;

                ChangeColorToBlack(hit.transform.gameObject);
            }
        }
        else
        {
            if (lastHitObject != null)
            {
                RestoreOriginalColor(lastHitObject);
                lastHitObject = null;
            }
        }
    }

    void ChangeColorToBlack(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material[] materials = renderer.materials;
            foreach (Material material in materials)
            {
                material.color = new Color (0f, 0f, 1f, .4f);
            }
        }
    }

    void RestoreOriginalColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material[] materials = renderer.materials;
            foreach (Material material in materials)
            {
                material.color = originalColor;
            }
        }
    }
}