using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInteraction : AbstractInteraction
{
    [SerializeField] private GameObject shopUi;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> instantiatedShopItems;
    [SerializeField] private GameObject shopItemTemplate;
    
    [SerializeField]
    [SerializedDictionary("Shop item", "Infos (key: container, value: amount)")]
    private SerializedDictionary<Ingredient, SerializedKeyValuePair<Slot, uint>> shopItems;

    private Transform _shopUiContent;

    void Start()
    {
        _shopUiContent = shopUi.GetComponentInChildren<ContentSizeFitter>().transform;
        FillShop();
    }
    
    public override void MainInteraction(GameObject author)
    {
        shopUi.SetActive(!shopUi.activeSelf);
        Cursor.lockState = shopUi.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        playerController.SetIsInUi(shopUi.activeSelf);
    }

    public override void SecondaryInteraction(GameObject author) { }

    private void FillShop()
    {
        foreach (var shopItem in shopItems)
        {
            IngredientData ingredientData = shopItem.Key.ingredientData;
            Slot destinationSlot = shopItem.Value.Key;
            uint amount = shopItem.Value.Value;
            
            TMP_Text itemText = shopItemTemplate.transform.Find("name").GetComponent<TMP_Text>();
            itemText.text = $"{ingredientData.title} (x{amount})";

            TMP_Text priceText = shopItemTemplate.transform.Find("price").GetComponent<TMP_Text>();
            priceText.text = $"Prix: {ingredientData.price}";
            
            GameObject instantiatedShopItem = Instantiate(shopItemTemplate, _shopUiContent);
            instantiatedShopItem.name = ingredientData.title;
            instantiatedShopItem.SetActive(true);

            Button button = instantiatedShopItem.GetComponent<Button>();
            button.onClick.AddListener(() => OnClickOnShopItem(shopItem.Key.gameObject, ingredientData.price, amount, destinationSlot));
            
            instantiatedShopItems.Add(instantiatedShopItem);
        }
    }

    private void OnClickOnShopItem(GameObject ingredient, float price, uint amount, Slot destinationSlot)
    {
        float totalPrice = price * amount;
        if (gameManager.RemoveMoney(totalPrice))
        {
            Debug.Log($"price: {price}, amount: {amount}");
            destinationSlot.Put(ingredient, amount);
        }
    }
}