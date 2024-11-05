using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellUnlockUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject buttonPrefab; // Assign the button prefab here
    public Transform buttonParent; // The parent object for buttons (the Panel)
    public List<Spell> spells; // List of spells to display

    void Start()
    {
        // Generate the spell buttons based on the spells list
        GenerateSpellButtons();
    }

    void GenerateSpellButtons()
    {
        foreach (var spell in spells)
        {
            // Instantiate a new button from the prefab
            GameObject buttonObject = Instantiate(buttonPrefab, buttonParent);
            Button button = buttonObject.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();

            // Set the button text to the spell name and unlocks info
            int unlockCount = 0; // Get this from your unlock manager
            buttonText.text = $"{spell.spellID}\n({unlockCount} of {5})";

            // Add a listener to handle button clicks for unlocking
            button.onClick.AddListener(() => UnlockSpell(spell.spellID));
        }
    }

    void UnlockSpell(string spellID)
    {
        // Handle the unlocking logic here
        Debug.Log($"Unlocking spell: {spellID}");
        // Implement unlocking logic using the existing SpellUnlocks logic
    }
}
