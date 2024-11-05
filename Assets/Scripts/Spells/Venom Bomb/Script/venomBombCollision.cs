using UnityEngine;

public class venomBombCollision : MonoBehaviour
{
    public GameObject venomPuddlePrefab;
    public float puddleDuration = 6f;
    private int puddleDamage;

    // Set the damage that the puddle will deal over time
    public void SetPuddleDamage(int damage)
    {
        puddleDamage = damage;
    }

    // Detect collision with any object
    void OnCollisionEnter(Collision collision)
    {
        // Use a Raycast to detect the ground position below the bomb
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Instantiate the Venom Puddle at the ground position (hit point)
            GameObject venomPuddle = Instantiate(venomPuddlePrefab, hit.point, Quaternion.identity);

            // Pass the damage value to the puddle
            VenomPuddle puddleScript = venomPuddle.GetComponent<VenomPuddle>();
            if (puddleScript != null)
            {
                puddleScript.SetDamage(puddleDamage);  // Set the damage in the puddle
            }

            // Destroy the Venom Puddle after the specified duration
            Destroy(venomPuddle, puddleDuration);
        }

        // Destroy the Venom Bomb object after the collision
        Destroy(gameObject);
    }
}
