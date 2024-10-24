using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public GameObject spellPrefab;  // Prefab for the spell
    public float cooldownDuration;  // Duration of the cooldown
    public float spawnOffsetDistance = 2.0f;  // Distance in front of the camera
    public string spellID;  // Unique identifier for the spell
    public int maxLevel = 5;  // Maximum unlock level
    public int minPlayerLevel = 1;  // Minimum player level required to unlock this spell
    public int currentLevel = 0;  // Current unlock level, starts at 0

    // Define level-based damage (can be customized in subclasses)
    public virtual float GetDamage()
    {
        return 10 * currentLevel;  // Example scaling: base damage increases per level
    }

    public abstract void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation);  // Must be overridden

    // Method to set the current level of the spell (called when unlocked)
    public void SetLevel(int level)
    {
        currentLevel = Mathf.Clamp(level, 0, maxLevel);  // Clamp between 0 and maxLevel
        Debug.Log($"{spellID} set to level {currentLevel}, damage: {GetDamage()}");
    }
}
