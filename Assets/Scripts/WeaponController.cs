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

    public Camera mainCamera; 

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

        
        StartCoroutine(ResetAttackCooldown(HeavyAttackCooldown));
    }

   
    IEnumerator ResetAttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        CanAttack = true;
    }
}
