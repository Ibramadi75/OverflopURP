using UnityEngine;

public class Order : MonoBehaviour
{
    public delegate void OnExpire(Order expiredOrder);
    public OnExpire onExpire;
    
    [SerializeField] private Countdown countdown;

    private Recipe _recipe;

    public Recipe GetRecipe()
    {
        return _recipe;
    }

    void Start()
    {
        _recipe = GetComponent<Recipe>();

        countdown.onComplete += OnCountdownComplete;
        countdown.SetTime(_recipe.GetBaseExpiration());
        countdown.gameObject.SetActive(true);
    }

    private void OnCountdownComplete()
    {
        onExpire?.Invoke(this);
    }
}