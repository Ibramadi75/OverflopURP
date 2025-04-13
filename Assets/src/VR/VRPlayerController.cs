using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRPlayerController : MonoBehaviour
{
    [Header("Déplacement & Contrôles")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Transform headTransform;

    [Header("Effets météo")]
    [SerializeField] private ParticleSystem rain;
    [SerializeField] private ParticleSystem wind;
    [SerializeField] private float rainOffsetDistance = 1.5f;
    [SerializeField] private float rainHeight = 25f;
    [SerializeField] private float windHeight = 1f;
    [SerializeField] private bool allowRain = true;
    [SerializeField] private bool allowWind = true;

    [Header("Références")]
    [SerializeField] private Persistent persistent;
    [SerializeField] private AudioSource _audioSource;

    [Header("UI Text")]
    public string textObjectName = "AnothernightText";
    public float fadeDuration = 2f;
    public float visibleDuration = 3f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;          // Vector2 - joystick gauche
    [SerializeField] private InputActionReference rotateAction;        // Vector2 - joystick droit
    [SerializeField] private InputActionReference triggerAction;       // float ou bouton - gâchette
    [SerializeField] private InputActionReference primaryButtonAction; // bouton A ou X

    private bool _isInUi;
    private bool _isRaining;
    private bool _isWindy;
    private bool _isMoving;

    public Camera PlayerCam { get; private set; }
    public Persistent Persitent => persistent;

    void Start()
    {
        PlayerCam = Camera.main;

        if (headTransform == null && PlayerCam != null)
            headTransform = PlayerCam.transform;

        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        // Enable input actions
        moveAction?.action.Enable();
        rotateAction?.action.Enable();
        triggerAction?.action.Enable();
        primaryButtonAction?.action.Enable();

        triggerAction.action.performed += OnTriggerPressed;
        primaryButtonAction.action.performed += OnPrimaryButtonPressed;

        // UI fade text
        GameObject textObject = GameObject.Find(textObjectName);
        if (textObject != null)
        {
            TextMeshProUGUI textMeshPro = textObject.GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
                StartCoroutine(FadeInOutText(textMeshPro));
        }
    }

    void Update()
    {
        if (_isInUi) return;

        // Lecture du mouvement
        Vector3 moveInput = moveAction.action.ReadValue<Vector3>();
        Vector3 inputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        _isMoving = inputDirection.magnitude > 0.1f;

        if (_isMoving)
        {
            Quaternion headYaw = Quaternion.Euler(0, headTransform.eulerAngles.y, 0);
            Vector3 moveDirection = headYaw * inputDirection;
            transform.position += moveDirection * speed * Time.deltaTime;

            if (allowRain)
            {
                SetRaining(!Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 10f));
                UpdateRainPosition(moveDirection);
            }

            if (allowWind)
            {
                SetWindy(!Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 10f));
                UpdateWindPosition(moveDirection);
            }

            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        // Rotation avec joystick droit
        Vector3 rotateInput = rotateAction.action.ReadValue<Vector3>();
        if (Mathf.Abs(rotateInput.x) > 0.1f)
        {
            float rotationAmount = rotateInput.x * 60f * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
            Debug.Log($"Rotation appliquée : {rotationAmount}°");
        }

        // Test clavier (optionnel pour debug)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Debug.Log("TOUCHE ESPACE DÉTECTÉE - Mode clavier actif");

        if (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed)
            Debug.Log("TOUCHES WASD DÉTECTÉES - Mode clavier actif");
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        Debug.Log("✅ Gâchette pressée (trigger) !");
    }

    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("✅ Bouton principal (A ou X) pressé !");
    }

    void OnDestroy()
    {
        triggerAction.action.performed -= OnTriggerPressed;
        primaryButtonAction.action.performed -= OnPrimaryButtonPressed;
    }

    void SetRaining(bool isRaining)
    {
        _isRaining = isRaining;
        rain?.gameObject.SetActive(isRaining);
    }

    void SetWindy(bool isWindy)
    {
        _isWindy = isWindy;
        wind?.gameObject.SetActive(isWindy);
    }

    void UpdateRainPosition(Vector3 direction)
    {
        Vector3 offset = transform.position + direction.normalized * rainOffsetDistance;
        rain.transform.position = offset + Vector3.up * rainHeight;
    }

    void UpdateWindPosition(Vector3 direction)
    {
        Vector3 offset = transform.position;
        wind.transform.position = offset + Vector3.up * windHeight;
        wind.transform.rotation = headTransform.rotation;
    }

    public void SetIsInUi(bool isInUi)
    {
        _isInUi = isInUi;
    }

    private IEnumerator FadeInOutText(TextMeshProUGUI textMeshPro)
    {
        yield return StartCoroutine(FadeText(textMeshPro, 0, 1, fadeDuration));
        yield return new WaitForSeconds(visibleDuration);
        yield return StartCoroutine(FadeText(textMeshPro, 1, 0, fadeDuration));
    }

    private IEnumerator FadeText(TextMeshProUGUI textMeshPro, float startAlpha, float endAlpha, float duration)
    {
        Color color = textMeshPro.color;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t / duration);
            textMeshPro.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        textMeshPro.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
