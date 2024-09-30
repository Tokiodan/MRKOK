using System.Collections;
using UnityEngine;

public class DivinePillar : Spell
{
    public GameObject aoeIndicatorPrefab; // For the indicator
    public GameObject aoeLightPrefab; // For the light beam
    public float aoeDuration = 3f; // Duration of the AoE effect
    public int damageAmount = 20; // Amount of damage to deal

    private GameObject currentIndicator;
    private Vector3 targetPosition;
    private bool isPlacingAoE = false;

    private void Awake()
    {
        spellID = "DivinePillar"; // Unique identifier for the spell
    }

    void Update()
    {
        HandleAoEInput();
    }

    void HandleAoEInput()
    {
        SpellManager spellManager = FindObjectOfType<SpellManager>();

        // Check if the spell is on cooldown
        if (spellManager.IsSpellOnCooldown(spellID)) return;

        // Get the key from the SpellManager instead of hardcoding it
        if (Input.GetKeyDown(GetKeyMapping()) && !isPlacingAoE)
        {
            // Create the indicator
            currentIndicator = Instantiate(aoeIndicatorPrefab, Vector3.zero, Quaternion.identity);
            isPlacingAoE = true;
        }

        if (isPlacingAoE && currentIndicator != null)
        {
            // Follow the mouse position to move the indicator
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Update the indicator position but save the target position
                targetPosition = new Vector3(hit.point.x, -0.99f, hit.point.z);
                currentIndicator.transform.position = targetPosition; // Keep the indicator above the ground
            }
        }

        // Cast the spell when the key is released
        if (isPlacingAoE && Input.GetKeyUp(GetKeyMapping()))
        {
            // Destroy the indicator
            Destroy(currentIndicator);
            isPlacingAoE = false;

            // Create the light beam with a collider to handle damage
            GameObject aoeLight = Instantiate(aoeLightPrefab, targetPosition, Quaternion.identity);
            Destroy(aoeLight, aoeDuration); // Remove the light beam after AoE duration

            // Inform the SpellManager to start the cooldown
            spellManager.StartCooldown(spellID, cooldownDuration);
        }
    }

    // Function to get the assigned key from SpellManager
    private KeyCode GetKeyMapping()
    {
        SpellManager spellManager = FindObjectOfType<SpellManager>();
        foreach (var mapping in spellManager.spellMappings)
        {
            if (mapping.spell == this)
            {
                return mapping.key;
            }
        }
        return KeyCode.None; // Default if not found
    }

    public override void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // The actual spell casting logic is handled in HandleAoEInput
    }
}
