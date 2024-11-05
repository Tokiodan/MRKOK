using UnityEngine;

public class venomBomb : Spell
{
    public float launchForce = 10f;
    public float upwardForce = 5f;
    public GameObject venomPuddlePrefab;  // Prefab reference for the puddle

    private void Awake()
    {
        spellID = "VenomBomb";  // Unique ID for this spell
    }

    public override int GetDamage()
    {
        return baseDamage + (damageIncrement * currentLevel);  // Damage scales with spell level
    }

    public override void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject bomb = Instantiate(spellPrefab, spawnPosition, spawnRotation);
        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();
        if (bombRb != null)
        {
            Vector3 force = spawnRotation * Vector3.forward * launchForce + Vector3.up * upwardForce;
            bombRb.AddForce(force, ForceMode.Impulse);
        }

        // Pass the current damage to the bomb script, so the puddle can inherit the value
        venomBombCollision bombCollision = bomb.GetComponent<venomBombCollision>();
        if (bombCollision != null)
        {
            bombCollision.SetPuddleDamage(GetDamage());
        }
    }
}
