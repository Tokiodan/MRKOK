using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword; // Single weapon for both attacks
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public float HeavyAttackCooldown = 2.0f; // Longer cooldown for Heavy attack
    public AudioClip SwordAttackSound;

    public Transform playerTransform; // Reference to the player's transform
    public Camera mainCamera; // Reference to the main camera

    private void Update()
    {
        // Left click for regular Sword attack
        if (Input.GetMouseButtonDown(1))
        {
            if (CanAttack)
            {
                FaceCameraDirection(); // Rotate to face the camera's forward direction
                SwordAttack();
            }
        }

        // Right click for HeavySlab attack (now called Sword-Heavy)
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                FaceCameraDirection(); // Rotate to face the camera's forward direction
                HeavySlabAttack();
            }
        }
    }

    [SerializeField]
    void SwordAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack"); // Regular attack animation trigger
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(SwordAttackSound);
        // Start cooldown coroutine for Sword attack
        StartCoroutine(ResetAttackCooldown(AttackCooldown));
    }

    [SerializeField]
    void HeavySlabAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Sword-Heavy"); // Heavy attack animation trigger

        // Start cooldown coroutine for HeavySlab attack with longer cooldown
        StartCoroutine(ResetAttackCooldown(HeavyAttackCooldown));
    }

    // Rotate the player to face the camera's forward direction
    void FaceCameraDirection()
    {
        // Get the forward direction of the camera, ignoring the y-axis so the player doesn't tilt upwards or downwards
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0f; // Lock the y-axis to avoid tilting up or down

        // If there is a non-zero forward vector, rotate the player
        if (forward != Vector3.zero)
        {
            playerTransform.rotation = Quaternion.LookRotation(forward);
        }
    }

    // A single method for resetting attack cooldowns
    IEnumerator ResetAttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        CanAttack = true;
    }
}
