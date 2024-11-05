using System.Collections;
using UnityEngine;

public class DivinePillar : Spell
{
    public GameObject aoeIndicatorPrefab; // For the indicator
    public GameObject aoeLightPrefab; // For the light beam
    public float aoeDuration = 3f; // Duration of the AoE effect

    private GameObject currentIndicator;
    private Vector3 targetPosition;
    private bool isPlacingAoE = false;

    private SpellManager spellManager;

    private void Awake()
    {
        spellID = "DivinePillar"; // Unique identifier for the spell
        cooldownDuration = 5f; // Set cooldown for DivinePillar
        baseDamage = 30; // Set base damage
        damageIncrement = 10; // Set damage increment per level
        SetLevel(currentLevel); // Initialize damage based on current level

        spellManager = FindObjectOfType<SpellManager>();
    }

    void Update()
    {
        HandleAoEInput();
    }

    void HandleAoEInput()
    {
        // Get the key from the SpellManager instead of hardcoding it
        KeyCode keyMapping = GetKeyMapping();

        // Check if the spell is on cooldown
        if (spellManager.IsSpellOnCooldown(spellID) && !isPlacingAoE) return;

        // Allow the indicator to be placed even if the spell is locked
        if (Input.GetKeyDown(keyMapping) && !isPlacingAoE)
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
        if (isPlacingAoE && Input.GetKeyUp(keyMapping))
        {
            // Destroy the indicator
            Destroy(currentIndicator);
            isPlacingAoE = false;

            // Check if the spell is unlocked before casting
            if (!spellManager.IsSpellUnlocked(spellID))
            {
                Debug.Log($"{spellID} has not been unlocked yet!");
                return; // Exit if not unlocked
            }

            // Create the light beam with a collider to handle damage
            GameObject aoeLight = Instantiate(aoeLightPrefab, targetPosition, Quaternion.identity);

            // Pass the calculated damage based on the current level to the AoE object
            PillarCollision pillarCollision = aoeLight.GetComponent<PillarCollision>();
            if (pillarCollision != null)
            {
                pillarCollision.SetDamageAmount(damage); // Apply the calculated damage
            }

            // Remove the light beam after AoE duration
            Destroy(aoeLight, aoeDuration);

            // Inform the SpellManager to start the cooldown after casting
            spellManager.StartCooldown(spellID, cooldownDuration);
        }
    }

    // Function to get the assigned key from SpellManager
    private KeyCode GetKeyMapping()
    {
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
        // If you ever need to do something additional on casting, you can do it here.
    }
}
