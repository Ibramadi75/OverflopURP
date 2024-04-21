using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private BaseCountdown countdown;
    
    private Recipe _recipe;
    public Recipe recipe => _recipe;
    
    // Start is called before the first frame update
    void Start()
    {
        _recipe = GetComponent<Recipe>();
        
        countdown.SetTime(_recipe.GetBaseExpiration());
        countdown.gameObject.SetActive(true);
    }

    public bool HasExpired() => countdown.GetRemainingTime() <= 0;

    public bool IsRecipeTitleIs(string title) => _recipe.GetTitle().Equals(title);
}
