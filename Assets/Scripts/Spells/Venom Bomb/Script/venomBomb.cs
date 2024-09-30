using UnityEngine;

public class VenomBomb : Spell
{
    public float launchForce = 10f;
    public float upwardForce = 5f;

    private void Awake()
    {
        spellID = "VenomBomb"; // Set a unique ID for this spell
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
    }
}



