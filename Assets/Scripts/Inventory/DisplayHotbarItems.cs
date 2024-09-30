using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DisplayHotbarItems : MonoBehaviour
{
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int inventorySlots;
    public GameObject ItemPrefab;
    public GameObject SelectPrefab;
    [SerializeField] private GameObject activePrefab;

    public InventoryObject inventory;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    GameObject[] itemCount;

    private InventorySlot CurrentSelected;

    void Start()
    {
        itemCount = new GameObject[inventorySlots];
        CreateDisplay();
        UpdateSelectedSlot(itemCount[0]);
    }

    void Update()
    {
        UpdateDisplay();
        SelectSlot();
        SetAttack();
    }

    public void SelectSlot()
    {
        // 1.press button
        // 2.find slot according to button
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateSelectedSlot(itemCount[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateSelectedSlot(itemCount[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateSelectedSlot(itemCount[2]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateSelectedSlot(itemCount[3]);
        }


        // 4. execute the item on click
    }

    void SetAttack()
    {
        ItemObject item = inventory.database.GetItem[CurrentSelected.item.Id];
        if (item.type == ItemType.Magic && item is MagicObject Spell)
        {

            // get the prefab with the attack from the item.
            // add it to the player
            Debug.Log("setting script");
            MagicAttack spellScript = Spell.spellprefab.GetComponent<MagicAttack>();

            if (PlayerController.FireMagic != spellScript.CastSpell)
            {
                Debug.Log("attack set");
                PlayerController.cooldownDuration = Spell.cooldown;
                PlayerController.FireMagic = spellScript.CastSpell;
            }
        }
        else
        {
            PlayerController.FireMagic = null;
        }
    }

    void UpdateSelectedSlot(GameObject obj)
    {
        if (activePrefab == null)
        {
            activePrefab = Instantiate(SelectPrefab, obj.transform.position, quaternion.identity, transform);
        }
        // 3. move selectedItem prefab to item
        activePrefab.transform.position = obj.transform.position;
        CurrentSelected = itemsDisplayed[obj];
    }

    void UpdateDisplay()
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

    void CreateDisplay()
    {
        for (int i = 0; i < inventorySlots; i++)
        {
            var obj = Instantiate(ItemPrefab, Vector3.zero, quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            itemCount[i] = obj;
        }
    }


    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * i), Y_START);
    }
}
