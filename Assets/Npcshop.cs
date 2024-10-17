using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcShop : MonoBehaviour
{
    public GameObject shopPanel;
    private bool isShopOpen = false;
    private bool isPlayerInRange = false; // Keeps track if the player is nearby

    private void Update()
    {
        // If player presses B and is within range of the NPC
        if (Input.GetKeyDown(KeyCode.B) && isPlayerInRange)
        {
            if (isShopOpen)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }

    private void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
        isShopOpen = true;
        Cursor.lockState = CursorLockMode.Confined; // Free the cursor to interact with UI
        Cursor.visible = true;
    }

    private void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
        isShopOpen = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back to the game
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (isShopOpen)
            {
                CloseShop(); // Close the shop if the player leaves the range
            }
        }
    }
}
