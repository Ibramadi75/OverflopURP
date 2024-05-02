using UnityEngine;

public class MeltInteraction : AbstractInteraction
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

    public override void MainInteraction(GameObject author)
    {
        var authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
        {
            slot.Put(authorSlot.Get());

            _ingredient = slot.GetObjectInSlot().GetComponent<Ingredient>();
            if (_ingredient is not null && _ingredient.ingredientData.isMeltable)
            {
                countdown.SetTime(_ingredient.ingredientData.time);
                countdown.gameObject.SetActive(true);
            }
        }

        else if (!slot.IsEmpty() && authorSlot.IsEmpty())
        {
            authorSlot.Put(slot.Get());
        }
    }

    public override void SecondaryInteraction()
    {
    }

    protected override void OnCountdownComplete()
    {
        slot.Clear();
        slot.Put(_ingredient.ingredientData.meltedPrefab);
        if (!_audioSource.isPlaying) return;
        _audioSource.Stop();
    }

    protected override void WhileCountdownRunning()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}