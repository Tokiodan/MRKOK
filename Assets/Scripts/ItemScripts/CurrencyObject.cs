using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Currency Object", menuName = "Inventory/Items/Currency")]

public class CurrencyObject : ItemObject
{
    public int MoneyValue;
    private void Awake()
    {
        type = ItemType.Currency;
    }
}
