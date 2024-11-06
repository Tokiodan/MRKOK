using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingInterface : MonoBehaviour
{
    public List<GameObject> slots = new();
    public InventoryObject inventory;
    public RecipeList Recipies;
    public GameObject CraftButton;
    public InventorySlot[] inventorySlots = new InventorySlot[2];
    public static Dictionary<GameObject, InventorySlot> CraftingSlots;
    public UserPreference userpref;
    void Awake()
    {
        inventory = userpref.saves[userpref.ChosenSave].Inventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateCraftingSlots();
        CraftButton.GetComponent<Button>().onClick.AddListener(() => CraftItem());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in CraftingSlots)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<UnityEngine.UI.Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].itemImg;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("N0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<UnityEngine.UI.Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void CraftItem()
    {
        if (inventorySlots[0].ID < 0 && inventorySlots[1].ID < 0)
        {
            return;
        }
        ItemObject Crafted = Recipies.FindRecipe(inventorySlots[0].item.Id, inventorySlots[1].item.Id);
        inventory.AddItem(new Item(Crafted), 1);

        // clear the inventory slots.
        inventorySlots[0].ID = -1;
        inventorySlots[0].item = null;

        inventorySlots[1].ID = -1;
        inventorySlots[1].item = null;
        // #FIXME you can spam recipe.

    }



    void InstantiateCraftingSlots()
    {

        CraftingSlots = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < slots.Count; i++)
        {
            GameObject obj = slots[i];
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });

            CraftingSlots.Add(slots[i], inventorySlots[i]);
        }
    }

    // add hoverobj of the crafting.
    public void OnEnter(GameObject obj)
    {
        DisplayInventory.mouseItem.hoverObj = obj;
    }


    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var EventTrigger = new EventTrigger.Entry();
        EventTrigger.eventID = type;
        EventTrigger.callback.AddListener(action);
        trigger.triggers.Add(EventTrigger);
    }

}
