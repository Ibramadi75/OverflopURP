using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private XRNode leftHandNode = XRNode.LeftHand;
    [SerializeField] private XRNode rightHandNode = XRNode.RightHand;
    [SerializeField] private ParticleSystem rain;
    [SerializeField] private ParticleSystem wind;
    [SerializeField] private float rainOffsetDistance = 1.5f;
    [SerializeField] private float rainHeight = 25f;
    [SerializeField] private bool allowRain;
    [SerializeField] private bool allowWind;
    [SerializeField] private float windHeight = 1f;
    [SerializeField] private Persistent persistent;
    [SerializeField] private Transform headTransform; // Référence au transform de la caméra VR
    
    private bool _isInUi;
    private bool _isRaining;
    private bool _isWindy;
    private AudioSource _audioSource;
    
    // Variables pour les contrôleurs VR
    private Vector2 _leftThumbstickValue;
    private Vector2 _rightThumbstickValue;
    private bool _isMoving;

    public Camera PlayerCam { get; private set; }
    public Persistent Persitent => persistent;
    public ParticleSystem Rain
    {
        get => rain;
        set => rain = value;
    }

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
        
        // La caméra est maintenant gérée par le XR Rig, donc trouvez-la
        PlayerCam = FindObjectOfType<Camera>();
        
        // En VR, ne pas verrouiller le curseur
        Cursor.lockState = CursorLockMode.None;

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
        
        // Si headTransform n'est pas défini, utiliser le transform de la caméra
        if (headTransform == null && PlayerCam != null)
        {
            headTransform = PlayerCam.transform;
        }
    }

    void Update()
    {
        if (_isInUi) return;

        // Obtenir les valeurs des joysticks
        InputDevice leftHandDevice = InputDevices.GetDeviceAtXRNode(leftHandNode);
        bool leftStickSuccess = leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out _leftThumbstickValue);
        Debug.Log($"Left Controller: Device valid={leftHandDevice.isValid}, Name={leftHandDevice.name}, Joystick read success={leftStickSuccess}, Value=({_leftThumbstickValue.x}, {_leftThumbstickValue.y})");
        
        InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(rightHandNode);
        bool rightStickSuccess = rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out _rightThumbstickValue);
        Debug.Log($"Right Controller: Device valid={rightHandDevice.isValid}, Name={rightHandDevice.name}, Joystick read success={rightStickSuccess}, Value=({_rightThumbstickValue.x}, {_rightThumbstickValue.y})");
        
        // Utiliser le joystick gauche pour le mouvement
        Vector3 direction = new Vector3(_leftThumbstickValue.x, 0, _leftThumbstickValue.y);
        _isMoving = direction.magnitude >= 0.1f;
        
        if (_isMoving)
        {
            // Obtenir la direction de mouvement basée sur l'orientation de la tête
            Quaternion headYaw = Quaternion.Euler(0, headTransform.eulerAngles.y, 0);
            Vector3 moveDirection = headYaw * direction;
            
            // Déplacer le joueur
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
            
            if (_audioSource.isPlaying) return;
            _audioSource.Play();
        }
        else if (_audioSource.isPlaying) 
        {
            _audioSource.Stop();
        }
        
        // Exemple: utiliser le joystick droit pour tourner le joueur (rotation Y)
        if (Mathf.Abs(_rightThumbstickValue.x) > 0.1f)
        {
            float rotationAmount = _rightThumbstickValue.x * 60f * Time.deltaTime; // 60 degrés par seconde
            transform.Rotate(0, rotationAmount, 0);
            Debug.Log($"Rotation appliquée: {rotationAmount} degrés");
        }
        
        // Exemple: détecter l'appui sur un bouton (comme le trigger)
        bool triggerPressed = false;
        bool triggerSuccess = rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
        Debug.Log($"Trigger: Read success={triggerSuccess}, Pressed={triggerPressed}");
        
        if (triggerPressed)
        {
            Debug.Log("Trigger droit pressé - ACTION DÉCLENCHÉE");
            // Ajouter votre action ici
        }
        
        // Ajouter des logs pour les boutons principaux (A/B ou X/Y)
        bool primaryButtonPressed = false;
        bool primaryButtonSuccess = rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed);
        Debug.Log($"Bouton A/X: Read success={primaryButtonSuccess}, Pressed={primaryButtonPressed}");
        
        // Vérifier si nous sommes en mode simulateur/clavier
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("TOUCHE ESPACE DÉTECTÉE - Mode clavier actif");
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("TOUCHES WASD DÉTECTÉES - Mode clavier actif");
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
        wind.transform.rotation = headTransform.rotation;
    }

    public void SetIsInUi(bool isInUi)
    {
        _isInUi = isInUi;
    }
}