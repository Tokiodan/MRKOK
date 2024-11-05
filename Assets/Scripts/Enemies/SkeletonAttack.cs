using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public float damage;
    // public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerEntity>().TakePhysicalDmg(damage);
            }

           Debug.Log("Comparing...");
            PlayerEntity playerScript = other.GetComponent<PlayerEntity>();
            if (playerScript != null)
            {
                playerScript.TakePhysicalDmg(damage);
            }

            // Debug.Log("Comparing...");
            // PlayerController playerScript = other.GetComponent<PlayerController>();
            // if (playerScript != null)
            //  {
            //     playerScript.TakeDamage(damage);
            // }
        }
    }
}
