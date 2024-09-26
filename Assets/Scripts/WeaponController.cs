using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public float HeavyAttackCooldown = 2.0f;
    public AudioClip SwordAttackSound;

    private void Start()
    {
        // Initially hide the sword
        SetSwordVisibility(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            SwordAttack();
        }

        if (Input.GetMouseButtonDown(1) && CanAttack)
        {
            HeavySlabAttack();
        }
    }

    [SerializeField]
    void SwordAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");

        // Show the sword when attacking
        SetSwordVisibility(true);

        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(SwordAttackSound);

        StartCoroutine(ResetAttackCooldown(AttackCooldown));
    }

    [SerializeField]
    void HeavySlabAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Sword-Heavy");

        // Show the sword when attacking
        SetSwordVisibility(true);

        StartCoroutine(ResetAttackCooldown(HeavyAttackCooldown));
    }

    IEnumerator ResetAttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        CanAttack = true;
        // Optionally hide the sword again after the attack
        SetSwordVisibility(false);
    }

    private void SetSwordVisibility(bool isVisible)
    {
        // Enable or disable the sword's renderer
        MeshRenderer renderer = Sword.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }
}
