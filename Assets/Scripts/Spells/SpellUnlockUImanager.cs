using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellUnlockUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject buttonPrefab;  // Prefab for each button
    public Transform buttonParent;   // Parent container for the buttons (should have GridLayoutGroup)
    public SpellManager spellManager;  // Reference to SpellManager
    public GameObject uiPanel;  // The main UI panel
    public player player;  // Reference to the Player script to check player level

    private List<Button> spellButtons = new List<Button>();  // List to hold the generated buttons

    void Start()
    {
        // Show cursor when the UI starts
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Generate buttons for spells
        GenerateSpellButtons();
    }

    void GenerateSpellButtons()
    {
        // Clear existing buttons in case this gets called multiple times
        foreach (Button button in spellButtons)
        {
            Destroy(button.gameObject);  // Destroy existing button instances
        }
        spellButtons.Clear();

        // Create a button for each spell in the spell manager
        foreach (var mapping in spellManager.spellMappings)
        {
            // Check if the player can unlock the spell based on their level
            int playerLevel = player.currentLevel; // Assuming you have a currentLevel property in Player script
            int unlockLevel = spellManager.GetSpellLevel(mapping.spell.spellID);

            if (playerLevel >= mapping.spell.minPlayerLevel)
            {
                // Instantiate button for each spell
                GameObject buttonObject = Instantiate(buttonPrefab, buttonParent);
                Button button = buttonObject.GetComponent<Button>();
                TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();

                // Initialize button text with unlock info
                UpdateButtonUI(mapping.spell.spellID, buttonText);

                // Add listener to handle unlocking
                button.onClick.AddListener(() => UnlockSpell(mapping.spell.spellID, buttonText));
                
                // Add the button to the list of spell buttons
                spellButtons.Add(button);
            }
        }
    }

    // Unlock the spell and update the button text
    void UnlockSpell(string spellID, TextMeshProUGUI buttonText)
    {
        spellManager.UnlockSpell(spellID, player.currentLevel);
        UpdateButtonUI(spellID, buttonText);
    }

    // Update the button UI with the current unlock level and max level
    void UpdateButtonUI(string spellID, TextMeshProUGUI buttonText)
    {
        // Get the unlock level from the SpellManager instance
        int unlockLevel = spellManager.GetSpellLevel(spellID);

        // Update button text using the class name to reference MaxUnlockLevel
        buttonText.text = $"{spellID} (Level: {unlockLevel}/{SpellManager.MaxUnlockLevel})";
    }

    // Show the UI panel and unlock the cursor
    public void OpenUIPanel()
    {
        uiPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Hide the UI panel and lock the cursor
    public void CloseUIPanel()
    {
        uiPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Refresh the UI, updating all buttons
    public void RefreshUI()
    {
        // Refresh the button UI for all spells
        foreach (Button button in spellButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            string spellID = buttonText.text.Split(' ')[0];
            UpdateButtonUI(spellID, buttonText);
        }
    }
}
