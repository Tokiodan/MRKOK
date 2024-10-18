/*using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [System.Serializable]
    public class SpellMapping
    {
        public KeyCode key;  // Key for casting the spell
        public Spell spell;  // Reference to the spell instance
    }

    public List<SpellMapping> spellMappings;  // Spells and their associated keycodes
    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
    private Dictionary<string, int> spellUnlockLevels = new Dictionary<string, int>();  // Track spell levels

    private bool timeStopped = false;  // Track if time is stopped

    public const int MaxUnlockLevel = 5;  // Make this public for access

    void Update()
    {
        if (timeStopped) return;

        HandleCooldowns();
        CheckForSpellCasts();
    }

    private void HandleCooldowns()
    {
        List<string> keys = new List<string>(cooldowns.Keys);
        foreach (string key in keys)
        {
            cooldowns[key] -= Time.deltaTime;
            if (cooldowns[key] <= 0) cooldowns[key] = 0;
        }
    }

    private void CheckForSpellCasts()
    {
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
        // Allow DivinePillar to be cast regardless of unlock level
        if (spell.spellID != "DivinePillar" && !IsSpellUnlocked(spell.spellID))
        {
            Debug.Log($"{spell.spellID} has not been unlocked yet!");
            return;
        }

        if (IsSpellOnCooldown(spell.spellID))
        {
            Debug.Log($"{spell.spellID} is on cooldown for {cooldowns[spell.spellID]:0.0} seconds.");
            return;
        }

        if (spell.currentLevel < 1 && spell.spellID != "DivinePillar")
        {
            Debug.Log($"{spell.spellID} is not unlocked or below minimum cast level.");
            return;
        }

        // Log the damage the spell will do
        Debug.Log($"{spell.spellID} casting at level {spell.currentLevel}, damage: {spell.GetDamage()}");

        // Default spell-casting logic
        Camera mainCamera = Camera.main;
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spell.spawnOffsetDistance;
        spell.CastSpell(spawnPosition, mainCamera.transform.rotation);

        // Start cooldown for spell
        StartCooldown(spell.spellID, spell.cooldownDuration);
    }

    public bool IsSpellUnlocked(string spellID)
    {
        return spellUnlockLevels.ContainsKey(spellID) && spellUnlockLevels[spellID] >= 1;
    }

    public bool IsSpellOnCooldown(string spellID)
    {
        return cooldowns.ContainsKey(spellID) && cooldowns[spellID] > 0;
    }

    public void StartCooldown(string spellID, float duration)
    {
        cooldowns[spellID] = duration;
    }

    public void UnlockSpell(string spellID, int playerLevel)
    {
        if (!spellUnlockLevels.ContainsKey(spellID)) spellUnlockLevels[spellID] = 0;

        Spell spell = GetSpellByID(spellID);
        if (spell != null && playerLevel >= spell.minPlayerLevel)
        {
            if (spellUnlockLevels[spellID] < MaxUnlockLevel)
            {
                spellUnlockLevels[spellID]++;
                Debug.Log($"{spellID} unlocked! Current Level: {spellUnlockLevels[spellID]}");

                // Update the spell's level in the spell instance
                spell.SetLevel(spellUnlockLevels[spellID]);
            }
            else
            {
                Debug.Log($"{spellID} is at maximum level!");
            }
        }
        else
        {
            Debug.Log($"Cannot unlock {spellID}. Player level {playerLevel} is below the requirement of {spell.minPlayerLevel}.");
        }
    }

    public int GetSpellLevel(string spellID)
    {
        return spellUnlockLevels.TryGetValue(spellID, out int level) ? level : 0;
    }

    // Helper function to get a spell by its ID
    public Spell GetSpellByID(string spellID)
    {
        foreach (var mapping in spellMappings)
        {
            if (mapping.spell.spellID == spellID)
            {
                return mapping.spell;
            }
        }
        return null;
    }
}
*/
