using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public GameObject spellPrefab;
    public float cooldownDuration;
    public float spawnOffsetDistance = 2.0f;
    public string spellID;
    public int maxLevel = 5;
    public int minPlayerLevel = 1;
    public int currentLevel = 1; // Start at 1 for proper level handling
    public int baseDamage; // Base damage defined in subclasses
    public int damageIncrement; // Damage increment defined in subclasses

    // Calculate damage based on level
    public virtual int GetDamage()
    {
        return baseDamage + (currentLevel - 1) * damageIncrement; // Calculate total damage
    }

    // Add a read-only property for damage
    public int damage => GetDamage(); // This provides access to the calculated damage

    public abstract void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation);

    // Method to set the current level of the spell
    public void SetLevel(int level)
    {
        currentLevel = Mathf.Clamp(level, 1, maxLevel); // Ensure it starts at 1
        Debug.Log($"{spellID} set to level {currentLevel}, damage: {GetDamage()}");
    }
}
