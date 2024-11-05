using UnityEngine;

public class fireball : Spell
{
    public float speed = 20f;
    public float fireballSpacing = 1.5f;

    private void Awake()
    {
        spellID = "Fireball";
        baseDamage = 20;
        damageIncrement = 10;
    }

    public override void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Calculate damage based on the current level
        int damage = baseDamage + (damageIncrement * currentLevel);

        if (currentLevel == maxLevel)
        {
            // Create three fireballs
            for (int i = -1; i <= 1; i++)
            {
                Vector3 positionOffset = spawnRotation * new Vector3(i * fireballSpacing, 0, 0);
                Vector3 fireballPosition = spawnPosition + positionOffset;
                if (i == 0) fireballPosition = spawnPosition;

                GameObject fireball = Instantiate(spellPrefab, fireballPosition, spawnRotation);
                FireballCollision fireballCollision = fireball.GetComponent<FireballCollision>();
                if (fireballCollision != null)
                {
                    fireballCollision.damage = damage; // Set calculated damage
                }

                Rigidbody rb = fireball.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = spawnRotation * Vector3.forward * speed;
                }
            }
        }
        else
        {
            // Create a single fireball for lower levels
            GameObject fireball = Instantiate(spellPrefab, spawnPosition, spawnRotation);
            FireballCollision fireballCollision = fireball.GetComponent<FireballCollision>();
            if (fireballCollision != null)
            {
                fireballCollision.damage = damage; // Set calculated damage
            }
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = spawnRotation * Vector3.forward * speed;
            }
        }
    }
}
