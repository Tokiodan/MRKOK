using UnityEngine;
using UnityEngine.AI;

public class Basic_attack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    public GameObject player;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private float nextAttackTime = 0f;
    [SerializeField] private Collider Collider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (Collider != null)
        {
            Collider.enabled = false;
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
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

    private void Attack()
    {
        animator.SetTrigger("Attack01");
        nextAttackTime = Time.time + attackCooldown;

        if (Collider != null)
        {
            Collider.enabled = true;
            StartCoroutine(DisableSwordColliderAfterAnimation());
        }
    }

    private System.Collections.IEnumerator DisableSwordColliderAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Collider.enabled = false;
    }

    public bool IsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01");
    }
}
