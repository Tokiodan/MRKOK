using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic_attack : MonoBehaviour
{
    public float damage = 10.0f;
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    public GameObject player;

    private player playerScript;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private float nextAttackTime = 0f;

    // SerializeField allows you to assign this in the inspector
    [SerializeField] private Collider attackCollider;

    void Start()
    {
        playerScript = player.GetComponent<player>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Disable the attack collider at the start
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    void Update()
    {
        float distanceToPlayerSqr = (transform.position - player.transform.position).sqrMagnitude;

        if (distanceToPlayerSqr <= attackRange * attackRange)
        {
            navMeshAgent.isStopped = true;

            if (Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack01");
        nextAttackTime = Time.time + attackCooldown;

        // Enable the attack collider briefly during the attack
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
            StartCoroutine(DisableAttackCollider());
        }
    }

    private IEnumerator DisableAttackCollider()
    {
        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Disable collider after attack
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsAttacking())
        {
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Player script reference is missing!");
            }
        }
    }

    private bool IsAttacking()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is missing!");
            return false;
        }

        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue; // Visual Debug: show attack range
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
