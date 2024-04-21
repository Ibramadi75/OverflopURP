using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private Countdown countdown;

    public Recipe recipe { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        recipe = GetComponent<Recipe>();

        countdown.SetTime(recipe.GetBaseExpiration());
        countdown.gameObject.SetActive(true);
    }

    public bool HasExpired()
    {
        return countdown.IsFinished();
    }

    public bool IsRecipeTitleIs(string title)
    {
        return recipe.GetTitle().Equals(title);
    }
}