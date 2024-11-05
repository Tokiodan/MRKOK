using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BBEG : Entity
{
    public string BBEGName;                   // Name of the BBEG
    public GameObject miasmaObject;           // Reference to the Miasma object
    public GameObject fireballPrefab;         // Reference to the Fireball prefab
    public GameObject canvasToDestroy;        // Canvas to destroy upon defeat
    public Transform player;                  // Reference to the player
    public float fireballCooldown = 5f;       // Cooldown for fireball cast
    public float castDistance = 10f;          // Fireball's max tracking distance

    private NavMeshAgent agent;               // Reference to the NavMeshAgent for controlling movement
    private float fireballTimer = 0f;         // Timer for fireball cooldown
    private bool isCasting = false;           // Is BBEG currently casting
    public bool isMiasmaActive = false;       // Is Miasma currently active
    public bool BBEGisDead = false;

    // Public properties to expose maxHealth and currentHealth
    public float MaxHealth => maxHealth;      
    public float CurrentHealth => Health;     

    protected override void Awake()
    {
        base.Awake();
        if (miasmaObject != null)
        {
            miasmaObject.SetActive(false);    // Ensure Miasma is inactive at the start
        }
        agent = GetComponent<NavMeshAgent>(); // Get reference to the NavMeshAgent
    }

    protected override void Update()
    {
        base.Update();
        
        fireballTimer -= Time.deltaTime;      // Reduce cooldown timer
        
        // Check for death condition
        if (Health <= 0)
        {
            HandleDeath();
            return;
        }

        // Check if health is below or equal to 50% and Miasma is not active
        if (Health <= maxHealth * 0.5f && !isMiasmaActive)
        {
            ActivateMiasma();
        }

        // Fireball casting
        /*if (!isCasting && fireballTimer <= 0 && player != null)
        {
            StartCoroutine(CastFireball());
        }*/
    }

    private void HandleDeath()
    {
        Debug.Log(BBEGName + " has been defeated!");

        if (canvasToDestroy != null)
        {
            Destroy(canvasToDestroy);
            Debug.Log("Canvas destroyed.");
        }

        Destroy(gameObject); // Destroy BBEG object
    }

    // Fireball casting method
    /*private IEnumerator CastFireball()
{
    isCasting = true;
    fireballTimer = fireballCooldown;

    if (agent != null) agent.isStopped = true; // Stop movement
    Debug.Log(BBEGName + " is casting a fireball!");

    yield return new WaitForSeconds(1f); // Optional casting delay

    if (fireballPrefab != null)
    {
        GameObject fireball = Instantiate(fireballPrefab, transform.position + Vector3.up, Quaternion.identity);
        BBEGFireballTracking fireballScript = fireball.GetComponent<BBEGFireballTracking>();
        if (fireballScript != null)
        {
            fireballScript.player = player;          // Set the player reference
            fireballScript.maxTrackingDistance = castDistance; // Set max tracking distance
        }
    }

    if (agent != null) agent.isStopped = false; // Resume movement
    isCasting = false;
}*/

    // Method to activate Miasma for healing
    private void ActivateMiasma()
    {
        isMiasmaActive = true;
        Debug.Log(BBEGName + " has activated Miasma!");

        if (miasmaObject != null) miasmaObject.SetActive(true);
        if (agent != null) agent.isStopped = true;

        StartCoroutine(HealWhileMiasmaActive());
    }

    private IEnumerator HealWhileMiasmaActive()
    {
        yield return new WaitForSeconds(2); // Optional delay before healing

        float targetHealth = maxHealth * 0.9f;
        float healDuration = 5f;
        float healRate = (targetHealth - Health) / healDuration;
        float elapsed = 0f;

        while (elapsed < healDuration && Health < targetHealth)
        {
            Health += healRate * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Health = targetHealth;
        Debug.Log(BBEGName + " has finished healing to 90% health.");

        if (miasmaObject != null) miasmaObject.SetActive(false);
        if (agent != null) agent.isStopped = false;

        isMiasmaActive = false;
    }

    public void TakePhysicalDmg(int damage)
    {
        Health -= damage;
        Debug.Log(BBEGName + " took " + damage + " physical damage. Current health: " + Health);

        if (Health <= 0)
        {
            HandleDeath();
            BBEGisDead = true;
        }
    }
}
