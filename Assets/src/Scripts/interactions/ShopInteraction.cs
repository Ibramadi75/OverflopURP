using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInteraction : AbstractInteraction
{
    [SerializeField] private GameObject shopUi;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private List<IngredientData> shopItems;
    [SerializeField] private List<GameObject> instantiatedShopItems;
    [SerializeField] private GameObject shopItemTemplate;
    
    public override void MainInteraction(GameObject author)
    {
        shopUi.SetActive(!shopUi.activeSelf);
        if (shopUi.activeSelf)
        {
            FillShop();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            ClearShop();
            Cursor.lockState = CursorLockMode.Locked;
        }

        playerController.SetIsInUi(shopUi.activeSelf);
    }

    public override void SecondaryInteraction(GameObject author) { }

    private void FillShop()
    {
        Vector2 firstPosition = shopItemTemplate.transform.position;
        foreach (IngredientData ingredientData in shopItems)
        {
            TMP_Text itemText = shopItemTemplate.transform.Find("name").GetComponent<TMP_Text>();
            itemText.text = ingredientData.title;

            Vector2 position = firstPosition - Vector2.up * (shopItems.IndexOf(ingredientData) * 50);
            
            GameObject instantiatedShopItem = Instantiate(shopItemTemplate, position, Quaternion.identity);
            instantiatedShopItem.transform.parent = shopUi.transform;
            instantiatedShopItem.SetActive(true);
            
            instantiatedShopItems.Add(instantiatedShopItem);
        }
    }

    private void ClearShop()
    {
        instantiatedShopItems.ForEach(instantiatedShopItem => Destroy(instantiatedShopItem));
        instantiatedShopItems.Clear();
    }
}