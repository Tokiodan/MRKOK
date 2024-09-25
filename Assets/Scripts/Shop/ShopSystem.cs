using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    private int item1Price = 4;
    private int item2Price = 8;
    private int item3Price = 6;

    private int purseAmount = 30; 

    // Inventory quantities
    private int item1Quantity = 2;
    private int item2Quantity = 5;
    private int item3Quantity = 3;

    public TextMeshProUGUI purseText;
    public TextMeshProUGUI item1QuantityText;
    public TextMeshProUGUI item2QuantityText;
    public TextMeshProUGUI item3QuantityText;

    public Button buyItem1Button;
    public Button buyItem2Button;
    public Button buyItem3Button;
    public Button sellItem1Button;
    public Button sellItem2Button;
    public Button sellItem3Button;

    void Start()
    {
        UpdatePurseText();
        UpdateItemQuantityText();

        buyItem1Button.onClick.AddListener(BuyItem1);
        buyItem2Button.onClick.AddListener(BuyItem2);
        buyItem3Button.onClick.AddListener(BuyItem3);
        sellItem1Button.onClick.AddListener(SellItem1);
        sellItem2Button.onClick.AddListener(SellItem2);
        sellItem3Button.onClick.AddListener(SellItem3);
    }

    public void BuyItem(int itemPrice, ref int itemQuantity)
    {
        if (purseAmount >= itemPrice)
        {
            purseAmount -= itemPrice;
            itemQuantity++;
            UpdatePurseText();
            UpdateItemQuantityText();
            Debug.Log("Item bought for " + itemPrice + ". New purse amount: " + purseAmount);
        }
        else
        {
            Debug.Log("Not enough money to buy the item.");
        }
    }

    public void SellItem(int itemPrice, ref int itemQuantity)
    {
        if (itemQuantity > 0)
        {
            purseAmount += itemPrice;
            itemQuantity--;
            UpdatePurseText();
            UpdateItemQuantityText();
            Debug.Log("Item sold for " + itemPrice + ". New purse amount: " + purseAmount);
        }
        else
        {
            Debug.Log("You don't have any items to sell.");
        }
    }

    private void UpdatePurseText()
    {
        purseText.text = "Purse: $" + purseAmount;
    }

    private void UpdateItemQuantityText()
    {
        item1QuantityText.text = "Quantity: " + item1Quantity;
        item2QuantityText.text = "Quantity: " + item2Quantity;
        item3QuantityText.text = "Quantity: " + item3Quantity;
    }

    public void BuyItem1() => BuyItem(item1Price, ref item1Quantity);
    public void BuyItem2() => BuyItem(item2Price, ref item2Quantity);
    public void BuyItem3() => BuyItem(item3Price, ref item3Quantity);
    
    public void SellItem1() => SellItem(item1Price, ref item1Quantity);
    public void SellItem2() => SellItem(item2Price, ref item2Quantity);
    public void SellItem3() => SellItem(item3Price, ref item3Quantity);
}
