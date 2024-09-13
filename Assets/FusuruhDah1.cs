using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusuruhDah : MonoBehaviour
{
    public Transform ParticleSpawnPoint;
    [SerializeField] GameObject FusuruhEffect;
    public float FusuruhSpeed = 1;
    public float OffsetDistance = 1.5f; // Offset afstand
    public float PushBackForce = 10f; // Kracht van de pushback
    public float LifeTime = 5f; // Tijd waarna het object wordt vernietigd
    public float PushRadius = 5f; // Straal waarin de pushback werkt

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           
            Vector3 spawnPosition = ParticleSpawnPoint.position + ParticleSpawnPoint.forward * OffsetDistance;

  
            GameObject fusuruhInstance = Instantiate(FusuruhEffect, spawnPosition, ParticleSpawnPoint.rotation);

          
            Rigidbody rb = fusuruhInstance.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(ParticleSpawnPoint.forward * FusuruhSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("FusuruhEffect heeft geen Rigidbody component.");
            }

            // Vernietig het object na een bepaalde tijd (5 seconden standaard)
            Destroy(fusuruhInstance, LifeTime);
        }
    }
}

public class FusuruhEffect : MonoBehaviour
{
    public float PushBackForce = 10f;
    public float PushRadius = 5f; // Straal waarin de pushback werkt

    private void Update()
    {
        // Zoek alle objecten binnen de straal van de PushRadius
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, PushRadius);

        foreach (Collider obj in nearbyObjects)
        {
            // Controleer of het object de tag "Enemy" heeft
            if (obj.CompareTag("Enemy"))
            {
                // Haal het Rigidbody-component op van de vijand
                Rigidbody enemyRb = obj.GetComponent<Rigidbody>();

                if (enemyRb != null)
                {
                    // Bereken de richting van de pushback (van de fusuruh naar de vijand)
                    Vector3 pushDirection = (obj.transform.position - transform.position).normalized;

                    // Voeg de kracht toe aan de vijand in de pushDirection
                    enemyRb.AddForce(pushDirection * PushBackForce, ForceMode.Impulse);
                }
            }
        }
    }
}
