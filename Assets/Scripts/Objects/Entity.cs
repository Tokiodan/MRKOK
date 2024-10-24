using UnityEngine;

public class Entity : MonoBehaviour
{
    public float Health = 100f;
    public float Resistance;
    public float MagResistance;

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakePhysicalDmg(float Damage)
    {
        float takenDamage = Damage - (0.75f * Resistance);
        Health -= takenDamage;
    }

    public void TakeMagicDmg(float Damage)
    {
        float takenDamage = Damage - (0.75f * MagResistance);
        Health -= takenDamage;
    }
}
