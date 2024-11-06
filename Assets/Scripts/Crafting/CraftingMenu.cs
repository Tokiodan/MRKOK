using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    public GameObject CraftingUI;
    [SerializeField] private GameObject ActiveCraftingUI;
    public static bool isCrafting;

    void Update()
    {
        // Close crafting ui
        if (Input.GetKeyDown(KeyCode.X) && ActiveCraftingUI != null)
        {
            CloseCraftingMenu();
        }
    }

    void CloseCraftingMenu()
    {
        // Close crafting ui
        Destroy(ActiveCraftingUI.gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        ActiveCraftingUI = null;
        Cursor.visible = false;
        isCrafting = false;

    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.gameObject.tag == "Player" && ActiveCraftingUI == null)
        {
            // open crafting menu
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isCrafting = true;
            ActiveCraftingUI = Instantiate(CraftingUI, transform.position, quaternion.identity);
        }
    }
}
