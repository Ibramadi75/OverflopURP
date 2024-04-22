using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float money;
    [SerializeField] private TMP_Text moneyText;

    private void Update()
    {
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
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