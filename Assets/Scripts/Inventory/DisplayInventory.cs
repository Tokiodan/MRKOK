using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;
public class DisplayInventory : MonoBehaviour
{

    public static MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory;

    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEM;

    public int X_START;
    public int Y_START;

    public GameObject ItemPrefab;
    public GameObject TextPrefab;
    [Range(100, -100)]
    public int Detail_Offset_Y = 100;
    public int InbetweenOffset = 10;
    public int Detail_Offset_X;

    public GameObject detailPanel;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    public UserPreference userpref;

    void Awake()
    {
        Debug.Log(userpref.ChosenSave);
        inventory = userpref.saves[userpref.ChosenSave].Inventory;
    }
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    //displays data of an item. (REQUIRES ITEM TO HAVE ITEMDETAILS() )
    public void DisplayDetails(Item item)
    {
        if (detailPanel == null)
        {
            return;
        }

        Dictionary<string, object> itemData = inventory.database.GetItem[item.Id].ItemDetails();
        itemData["name"] = item.Name;

        float prevOffset = 0;
        foreach (var data in itemData)
        {
            // instantiate text prefab.
            var textpref = Instantiate(TextPrefab, Vector3.zero, quaternion.identity, detailPanel.transform);
            textpref.transform.localPosition = new Vector3(0, Detail_Offset_Y - prevOffset, 0);
            textpref.transform.localScale = new Vector3(1, 1, 1);

            // add the data
            var textMesh = textpref.GetComponent<TextMeshProUGUI>();
            textMesh.GetComponent<TextMeshProUGUI>().text = data.Key.ToString() + ": " + data.Value.ToString();

            // save the offset
            textMesh.ForceMeshUpdate();
            Vector2 textSize = textMesh.GetPreferredValues();
            prevOffset += textSize.y + InbetweenOffset;

            // go next
        }
    }

    public void DeleteDisplay()
    {
        if (detailPanel == null)
        {
            return;
        }

        // deletes all text meshes, because i'm lazy and variable size.
        foreach (Transform child in detailPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //The reason why we are splitting the image and the item data itself is so the two are not interconnected. 
    //The img is only needed for graphics, not when computing in the background
    //Adds a small amount of abstraction for performance.
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
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


    public void CreateSlots()
    {
        // safety precaution, for strange things
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(ItemPrefab, Vector3.zero, quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragExit(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    // easier way to add events.
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var EventTrigger = new EventTrigger.Entry();
        EventTrigger.eventID = type;
        EventTrigger.callback.AddListener(action);
        trigger.triggers.Add(EventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            mouseItem.hoverItem = itemsDisplayed[obj];
            DisplayDetails(itemsDisplayed[obj].item);
        }
    }
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;

        DeleteDisplay();
    }

    public void OnDragStart(GameObject obj)
    {
        var mouseObj = new GameObject();
        var rt = mouseObj.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);
        mouseObj.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var image = mouseObj.AddComponent<UnityEngine.UI.Image>();
            image.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].itemImg;
            image.raycastTarget = false;
        }
        mouseItem.obj = mouseObj;
        mouseItem.Item = itemsDisplayed[obj];

    }
    public void OnDragExit(GameObject obj)
    {
        if (mouseItem.hoverObj && mouseItem.hoverObj.tag == "Untagged")
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else if (mouseItem.hoverObj.tag == "CraftingSlot")
        {
            inventory.MoveItem(itemsDisplayed[obj], CraftingInterface.CraftingSlots[mouseItem.hoverObj]);
        }
        else
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);
        mouseItem.obj = null;
    }

    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    // caculates the location of where the object is supposed to go
    public Vector3 GetPosition(int i)
    {

        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMNS)), Y_START + (-(Y_SPACE_BETWEEN_ITEM) * (i / NUMBER_OF_COLUMNS)), 0f);
    }

}

// FIXME temp measure
public class MouseItem
{
    public GameObject obj;
    public InventorySlot Item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
