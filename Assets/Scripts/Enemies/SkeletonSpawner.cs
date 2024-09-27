using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public int numberOfSkeletons = 5;
    public float spawnRadius = 20.0f;
    public float spawnInterval = 5.0f;

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnSkeletons();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnSkeletons()
    {
        for (int i = 0; i < numberOfSkeletons; i++)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            Instantiate(skeletonPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;

        // FIXME dit gaat niet werken watn de map is oneven. 
        // Dit kan opgelost worden door een findfloor functie volgensmij
        //Mogelijk even kijken naar een raycast draw om te kijken waar de vloer is.
        // Ik wil dit miss opakken om er een static method van te maken.
        randomDirection.y = KOKHelper.FloorPos(gameObject);  // Keep it on the ground level
        return transform.position + randomDirection; // Spawn around the spawner's position
    }

    // Draw the spawn radius in the Scene view using Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius); // Draw the spawn area
    }
}
