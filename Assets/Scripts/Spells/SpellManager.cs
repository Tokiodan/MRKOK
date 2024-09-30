using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [System.Serializable]
    public class SpellMapping
    {
        public KeyCode key; // Key for casting the spell
        public Spell spell;  // Spell instance
    }

    public List<SpellMapping> spellMappings; // List to hold spells and their keycodes
    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();

    void Update()
    {
        // Update cooldown timers
        List<string> keys = new List<string>(cooldowns.Keys);
        foreach (string key in keys)
        {
            cooldowns[key] -= Time.deltaTime;
            if (cooldowns[key] <= 0)
            {
                cooldowns[key] = 0; // Reset to zero if negative
            }
        }

        // Check for key presses to cast spells
        foreach (var mapping in spellMappings)
        {
            if (Input.GetKeyDown(mapping.key))
            {
                CastSpell(mapping.spell);
            }
        }
    }

    public void CastSpell(Spell spell)
    {
        // Check if the spell is on cooldown using the unique spellID
        if (IsSpellOnCooldown(spell.spellID))
        {
            Debug.Log($"{spell.spellID} is on cooldown for {cooldowns[spell.spellID]} seconds.");
            return; // Exit if on cooldown
        }

        // Handle specific spell casting logic for DivinePillar
        if (spell.spellID == "DivinePillar")
        {
            DivinePillar divinePillar = (DivinePillar)spell;
            divinePillar.CastSpell(Vector3.zero, Quaternion.identity); // Call the specific cast method
            return; // Cooldown is started after the spell is fully cast
        }

        // For other spells, default behavior
        Camera mainCamera = Camera.main;
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spell.spawnOffsetDistance;
        spell.CastSpell(spawnPosition, mainCamera.transform.rotation);
        StartCooldown(spell.spellID, spell.cooldownDuration); // Set the cooldown for other spells
    }

    public void StartCooldown(string spellID, float duration)
    {
        cooldowns[spellID] = duration;
    }

    public bool IsSpellOnCooldown(string spellID)
    {
        return cooldowns.ContainsKey(spellID) && cooldowns[spellID] > 0;
    }
}
