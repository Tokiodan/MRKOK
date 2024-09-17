using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEM;

    public int X_START;
    public int Y_START;

    public GameObject ItemPrefab;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {

        // instantiates a object for every item in your inventory and places them accordingly
        // sprites and amount are added after.
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            GameObject obj = Instantiate(ItemPrefab, gameObject.transform);
            obj.GetComponentInChildren<UnityEngine.UI.Image>().sprite = inventory.Container[i].item.itemImg;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0"); //I got told to do the tostring part so it looks nice later on.
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

    // caculates the location of where the object is supposed to go
    public Vector3 GetPosition(int i)
    {

        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMNS)), Y_START + (-(Y_SPACE_BETWEEN_ITEM) * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            //loops through all your items in your inventory.
            //if the item is already displayed, it updates the number.
            //if it's not, it adds it to the menu.
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                GameObject obj = Instantiate(ItemPrefab, gameObject.transform);
                Debug.Log(GetPosition(i).ToString());
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
}
