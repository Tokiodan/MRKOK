using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword; // The sword object
    public bool CanAttack = true; // Tracks if the player can attack
    public float AttackCooldown = 1.0f; // Cooldown between light attacks
    public AudioClip SwordAttackSound; // Sword attack sound
    public float PlayerDmg = 20f; // Damage dealt by the sword

    public Transform playerCamera; // Reference to the camera as a GameObject's Transform
    public float attackRange = 3.0f; // Range for sword attack
    public LayerMask attackLayerMask; // Layers to interact with during the attack

    private Coroutine attackCooldownCoroutine = null; // To track the attack cooldown coroutine
    private AudioSource audioSource;
    private Animator swordAnimator;

    private void Start()
    {
        // Initially hide the sword
        SetSwordVisibility(false);

        // Get the Animator on the sword, throw an error if it's missing
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
        // Left mouse button triggers the attack, but only if the player can attack
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            SwordAttack();
        }
    }

    void SwordAttack()
    {
        // Set CanAttack to false to prevent multiple attacks
        CanAttack = false;

        // Play the attack animation only if it's not already playing
        if (swordAnimator != null && !swordAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        {
            swordAnimator.SetTrigger("Attack");
        }

        // Make the sword visible during the attack
        SetSwordVisibility(true);

        // Play the sword attack sound if it's assigned and not already playing
        if (SwordAttackSound != null && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(SwordAttackSound);
        }

        // Perform the attack logic with a raycast from the camera's position and forward direction
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, attackRange, attackLayerMask))
        {
            Debug.Log("Sword hit: " + hit.collider.name);
            HandleHit(hit.collider);
        }
        else
        {
            Debug.Log("Sword missed, no target hit.");
        }

        // Start the cooldown coroutine to reset CanAttack after a delay
        if (attackCooldownCoroutine == null)
        {
            attackCooldownCoroutine = StartCoroutine(ResetAttackCooldown(AttackCooldown));
        }
    }

    private void HandleHit(Collider collider)
    {
        // Check if the collider belongs to an enemy
        if (collider.CompareTag("Enemy"))
        {
            SkeletonLogic skeleton = collider.GetComponent<SkeletonLogic>();
            if (skeleton != null)
            {
                skeleton.TakeHit(PlayerDmg); // Call the TakeHit method of the SkeletonLogic script with PlayerDmg
            }
        }
    }

    IEnumerator ResetAttackCooldown(float cooldown)
    {
        // Wait for the specified cooldown duration
        yield return new WaitForSeconds(cooldown);

        // Reset CanAttack to allow another attack
        CanAttack = true;
        attackCooldownCoroutine = null; // Reset coroutine reference
        Debug.Log("Attack cooldown finished, CanAttack reset to true.");

        // Optionally hide the sword again after the attack
        SetSwordVisibility(false);
    }

    // Sets the visibility of the sword's MeshRenderer
    private void SetSwordVisibility(bool isVisible)
    {
        MeshRenderer renderer = Sword.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }
}
