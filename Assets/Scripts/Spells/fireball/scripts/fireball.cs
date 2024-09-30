using UnityEngine;

public class Fireball : Spell
{
    public float speed = 20f; // Speed of the fireball

    private void Awake()
    {
        spellID = "Fireball"; // Set a unique ID for this spell
    }

    public override void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject fireball = Instantiate(spellPrefab, spawnPosition, spawnRotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawnRotation * Vector3.forward * speed;
        }
    }
}
