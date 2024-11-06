// WeaponController.cs (Handles sword attack and animation)
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public AudioClip SwordAttackSound;
    public Transform playerCamera;  // Reference to the camera for raycasting
    public float attackRange = 3.0f;
    public LayerMask attackLayerMask;
    private Coroutine attackCooldownCoroutine = null;

    [Header("Damage Settings")]
    public float PlayerDmg = 20f;

    private void Start()
    {
        SetSwordVisibility(false); // Hide sword initially
        Sword.GetComponent<SwordSlash>().Initialdamage = PlayerDmg;//VERY IMPORTANTE
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && CanAttack)
        {
            SwordAttack();
            CanAttack = true;
        }
    }

    void SwordAttack()
    {
        CanAttack = false;  // Disable attacking while sword is in use

        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");  // Trigger sword attack animation

        SetSwordVisibility(true);

        AudioSource ac = GetComponent<AudioSource>();
        if (SwordAttackSound != null)
        {
            ac.PlayOneShot(SwordAttackSound);  // Play sword sound
        }

        RaycastHit hit;

        // Perform a raycast from the player's camera position, in the direction it is facing
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, attackRange, attackLayerMask))
        {
            Debug.Log("Sword hit: " + hit.collider.name);

            // Apply damage to the target if it's an entity
            Entity targetEntity = hit.collider.GetComponent<Entity>();
            if (targetEntity != null)
            {
                targetEntity.TakePhysicalDmg(PlayerDmg);  // Apply physical damage
                Debug.Log("Dealt " + PlayerDmg + " damage to " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("Sword missed, no target hit.");
        }

        // Start cooldown coroutine
        if (attackCooldownCoroutine == null)
        {
            attackCooldownCoroutine = StartCoroutine(ResetAttackCooldown(AttackCooldown));
        }
    }

    IEnumerator ResetAttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);

        CanAttack = true;  // Allow attacking again after the cooldown
        attackCooldownCoroutine = null;
        SetSwordVisibility(false);  // Hide sword after attack
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
