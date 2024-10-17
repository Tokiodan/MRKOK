using UnityEngine;
using UnityEngine.AI;

public class Basic_attack : MonoBehaviour
{
    public float damage;
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    [SerializeField] private GameObject player;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private float nextAttackTime = 0f;

    // This makes absolutely no fucking sense LMFAO -z
    // if you public a serialized field, it does nothing.
    [SerializeField] private Collider Collider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // there will be one player anyway, why not just scan for the player tag? -Z
        player = GameObject.FindGameObjectWithTag("Player");

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
            // navMeshAgent.isStopped = false;
            //   navMeshAgent.SetDestination(player.transform.position);
        }

        // This is for the movement part of your animation. 
        // It makes skelly stop and go based on how fast you move -Z
        animator.SetFloat("MovementSpeed", navMeshAgent.velocity.magnitude);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerEntity>().TakePhysicalDmg(damage);
            }
            //  Debug.Log("Comparing...");
            // PlayerController playerScript = other.GetComponent<PlayerController>();
            // if (playerScript != null)
            //  {
            //     playerScript.TakeDamage(damage);
            //  }
        }
    }
}
