using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public float damage;
    // public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerEntity>().TakePhysicalDmg(damage);
        }
    }
}
