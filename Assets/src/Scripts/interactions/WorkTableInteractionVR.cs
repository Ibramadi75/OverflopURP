using UnityEngine;

public class WorkTableInteractionVR : AbstractInteraction
{
    [SerializeField] private Countdown countdown;

    private AudioSource _audioSource;
    private Ingredient _ingredient;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        countdown.onComplete += OnCountdownComplete;
        countdown.whileRunning += WhileCountdownRunning;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with work table.");

        if (collision.gameObject.CompareTag("Ingredient"))
        {
            Debug.Log("Ingredient collided with work table.");
            var ingredient = collision.gameObject.GetComponent<Ingredient>();
            if (slot.IsEmpty() && ingredient != null && ingredient.ingredientData.isCuttable)
            {
                Debug.Log("Slot is null? : " + slot);
                slot.Put(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Knife"))
        {
            var knife = collision.gameObject.GetComponent<Knife>();
            if (knife != null && knife.IsCutting())
            {
                if (!slot.IsEmpty() && slot.GetObjectInSlot().CompareTag("Ingredient"))
                {
                    var objectToCut = slot.GetObjectInSlot();
                    _ingredient = objectToCut.GetComponent<Ingredient>();

                    if (_ingredient != null && _ingredient.ingredientData.isCuttable)
                    {
                        countdown.SetIsTriggering(true);
                        if (!countdown.gameObject.activeSelf)
                        {
                            countdown.SetTime(_ingredient.ingredientData.time);
                            countdown.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public override void MainInteraction(GameObject author)
    {
    }

    public override void SecondaryInteraction()
    {
        if (slot.GetMaxCapacity() == 1 && !slot.IsEmpty())
        {
            var objectToCut = slot.GetObjectInSlot();
            _ingredient = objectToCut.GetComponent<Ingredient>();

            if (objectToCut.CompareTag("Ingredient") && _ingredient is not null &&
                _ingredient.ingredientData.isCuttable)
            {
                countdown.SetIsTriggering(true);
                if (!countdown.gameObject.activeSelf)
                {
                    countdown.SetTime(_ingredient.ingredientData.time);
                    countdown.gameObject.SetActive(true);
                }
            }
        }
    }

    protected override void OnCountdownComplete()
    {
        var cutObject = _ingredient.ingredientData.cutPrefab;
        slot.Clear();
        slot.Put(cutObject);
    }

    protected override void WhileCountdownRunning()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}