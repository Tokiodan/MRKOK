using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public GameObject spellPrefab; // Prefab for the spell
    public float cooldownDuration; // Duration of the cooldown
    public float spawnOffsetDistance = 2.0f; // Distance in front of the camera
    public string spellID; // Unique identifier for the spell

    public abstract void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation);
}
