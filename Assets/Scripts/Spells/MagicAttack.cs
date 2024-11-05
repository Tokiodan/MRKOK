[System.Serializable]
public class MagicAttack
{
    public string spellID;
    public float cooldownDuration;
    public int currentLevel;
    public int minPlayerLevel;

    // Example method for getting damage
    public float GetDamage()
    {
        // Logic to calculate damage based on current level
        return 10f * currentLevel; // Example calculation
    }

    public void CastSpell()
    {
        // Logic to cast the spell
        
    }
}
