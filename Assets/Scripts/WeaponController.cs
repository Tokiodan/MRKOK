using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword; // The sword object
    public bool CanAttack = true; // Tracks if the player can attack
    public float AttackCooldown = 1.0f; // Cooldown between attacks
    public AudioClip SwordAttackSound; // Sword attack sound
    public float PlayerDmg = 20f; // Damage dealt by the player

    public Transform playerCamera; // Reference to the camera
    public float attackRange = 3.0f; // Range for sword attack
    public LayerMask attackLayerMask; // Layers to interact with during the attack

    private Coroutine attackCooldownCoroutine = null; // To track the attack cooldown coroutine
    private AudioSource audioSource;
    private Animator swordAnimator;

    private void Start()
    {
        // Initially hide the sword
        SetSwordVisibility(false);

        // Get the Animator on the sword
        swordAnimator = Sword.GetComponent<Animator>();
        if (swordAnimator == null)
        {
            Debug.LogError("No Animator found on the Sword object. Please assign an Animator.");
        }

        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Left mouse button triggers the attack
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            SwordAttack();
        }
    }

    void SwordAttack()
    {
        // Set CanAttack to false to prevent multiple attacks
        CanAttack = false;

        // Play the attack animation
        if (swordAnimator != null && !swordAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        {
            swordAnimator.SetTrigger("Attack");
        }

        // Make the sword visible during the attack
        SetSwordVisibility(true);

        // Play the sword attack sound if it's assigned
        if (SwordAttackSound != null && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(SwordAttackSound);
        }

        // Perform the attack logic with a raycast
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, attackRange, attackLayerMask))
        {
            Debug.Log("Sword hit: " + hit.collider.name);
            HandleHit(hit.collider); // Call HandleHit to apply damage
        }
        else
        {
            Debug.Log("Sword missed, no target hit.");
        }

        // Start the cooldown coroutine
        if (attackCooldownCoroutine == null)
        {
            attackCooldownCoroutine = StartCoroutine(ResetAttackCooldown(AttackCooldown));
        }
    }

    private void HandleHit(Collider collider)
    {
        // Check if the collider belongs to an enemy
        if (collider.CompareTag("Skeleton"))
        {
            SkeletonLogic skeleton = collider.GetComponent<SkeletonLogic>();
            if (skeleton != null)
            {
                skeleton.TakeHit(PlayerDmg); // Call TakeHit with PlayerDmg
            }
        }
    }

    IEnumerator ResetAttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);

        CanAttack = true; // Reset CanAttack
        attackCooldownCoroutine = null; // Reset coroutine reference
        Debug.Log("Attack cooldown finished, CanAttack reset to true.");

        // Optionally hide the sword again after the attack
        SetSwordVisibility(false);
    }

    private void SetSwordVisibility(bool isVisible)
    {
        MeshRenderer renderer = Sword.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }
}
