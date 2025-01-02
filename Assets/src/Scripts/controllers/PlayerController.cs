using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sensitivity = 10.0f;
    [SerializeField] private ParticleSystem rain;
    [SerializeField] private ParticleSystem wind;
    [SerializeField] private float rainOffsetDistance = 1.5f;
    [SerializeField] private float rainHeight = 25f;
    [SerializeField] private bool allowRain;
    [SerializeField] private bool allowWind;
    [SerializeField] private float windHeight = 1f;
    [SerializeField] private Persistent persistent;
    
    private bool _isInUi;
    private bool _isRaining;
    private bool _isWindy;
    private AudioSource _audioSource;
    private float _rotationX;

    public Camera PlayerCam { get; private set; }
    public Persistent Persitent => persistent;


    public string textObjectName = "AnothernightText"; // Nom du GameObject
    public float fadeDuration = 2f;                   // Durée du fade-out
    public float visibleDuration = 3f;

    private IEnumerator FadeInOutText(TextMeshProUGUI textMeshPro)
    {
        // Fade-in
        yield return StartCoroutine(FadeText(textMeshPro, 0, 1, fadeDuration));

        // Temps visible
        yield return new WaitForSeconds(visibleDuration);

        // Fade-out
        yield return StartCoroutine(FadeText(textMeshPro, 1, 0, fadeDuration));
    }

        private IEnumerator FadeText(TextMeshProUGUI textMeshPro, float startAlpha, float endAlpha, float duration)
    {
        // Obtenez la couleur actuelle du texte
        Color textColor = textMeshPro.color;

        // Réduisez ou augmentez l'opacité sur le temps
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            textColor.a = Mathf.Lerp(startAlpha, endAlpha, normalizedTime);
            textMeshPro.color = textColor;

            yield return null; // Attendez le prochain frame
        }

        // Assurez-vous que l'opacité finale est correcte
        textColor.a = endAlpha;
        textMeshPro.color = textColor;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayerCam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;

        GameObject textObject = GameObject.Find(textObjectName);

        if (textObject != null)
        {
            // Récupérer le composant TextMeshPro
            TextMeshProUGUI textMeshPro = textObject.GetComponent<TextMeshProUGUI>();

            if (textMeshPro != null)
            {
                // Lancer le fade-out
                StartCoroutine(FadeInOutText(textMeshPro));
            }
            else
            {
                Debug.LogError("Le GameObject trouvé n'a pas de composant TextMeshProUGUI.");
            }
        }
        else
        {
            Debug.LogError($"Aucun GameObject trouvé avec le nom '{textObjectName}'.");
        }
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

            if (allowRain)
            {
                SetRaining(!Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 10f));
                UpdateRainPosition(direction);
            }

            if (allowWind)
            {
                SetWindy(!Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 10f));
                UpdateWindPosition(direction);
            }
            
            if (_audioSource.isPlaying) return;
            _audioSource.Play();
        }
        else if (_audioSource.isPlaying) {
            _audioSource.Stop();
        }
    }

    void SetRaining(bool isRaining)
    {
        _isRaining = isRaining;
        rain.gameObject.SetActive(isRaining);
    }

    void SetWindy(bool isWindy)
    {
        _isWindy = isWindy;
        wind.gameObject.SetActive(isWindy);
    }
    
    void UpdateRainPosition(Vector3 direction)
    {
        Vector3 offsetPosition = transform.position + direction.normalized * rainOffsetDistance;
        rain.transform.position = offsetPosition + Vector3.up * rainHeight;
    }

    void UpdateWindPosition(Vector3 direction)
    {
        Vector3 offsetPosition = transform.position;
        wind.transform.position = offsetPosition + Vector3.up * windHeight;
        wind.transform.rotation = PlayerCam.transform.rotation;
    }

    public void SetIsInUi(bool isInUi)
    {
        _isInUi = isInUi;
    }
}