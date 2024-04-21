using UnityEngine;

public class WorkTableInteraction : AbstractInteraction
{
    [SerializeField] private Countdown countdown;

    private AudioSource _audioSource;
    private Ingredient _ingredient;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        countdown.onComplete += OnCountdownComplete;
    }

    public override void MainInteraction(GameObject author)
    {
        var authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
            slot.Put(authorSlot.Get());

        else if (!slot.IsEmpty() && authorSlot.IsEmpty())
            authorSlot.Put(slot.Get());
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
}