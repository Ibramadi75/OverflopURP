using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float money = 0;
    [SerializeField] private TMP_Text moneyText;
    public float Money => money;

    void Update()
    {
        UpdateMoneyText();
    }

    void UpdateMoneyText()
    {
        moneyText.text = $"{money:F2}";
    }

    public void AddMoney(float amount)
    {
        money += amount;
        UpdateMoneyText();
    }

    public bool RemoveMoney(float amount)
    {
        var newMoney = money - amount;
        if (newMoney < 0) return false;
        money = newMoney;
        UpdateMoneyText();
        return true;
    }
}