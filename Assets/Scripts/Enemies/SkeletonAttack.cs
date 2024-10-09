using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Comparing...");
            PlayerController playerScript = other.GetComponent<PlayerController>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
        }
    }
}
