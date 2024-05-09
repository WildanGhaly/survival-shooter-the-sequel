using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Animator animator;
    private Transform player;

    public float detectionRadius = 5.0f;
    public float attackRange = 2.0f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;

        if (animator == null)
            Debug.LogError("Animator not found!");
        if (navMesh == null)
            Debug.LogError("NavMeshAgent not found!");
    }

    private void Start()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", 1f);
        }
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        List<Collider> enemyColliders = new List<Collider>();
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemyColliders.Add(collider);
            }
        }

        //Debug.Log("HIT COLLIDER" + enemyColliders.Count);
        if (enemyColliders.Count > 0)
        {
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (var hitCollider in enemyColliders)
            {
                float currentDistance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestEnemy = hitCollider.transform;
                }
            }

            if (closestEnemy != null && closestDistance <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                navMesh.SetDestination(closestEnemy.position);
                if (closestDistance <= attackRange)
                {
                    Attack(closestEnemy.GetComponent<Nightmare.YodaHealth>());
                }
            }
        }
        else
        {
            float distance = Vector3.Distance(navMesh.transform.position, player.position);
            if (distance > 3.0f)
            {
                navMesh.SetDestination(player.position);
                animator.SetBool("Attack", false);
            }
            else
            {
                navMesh.ResetPath();
                animator.SetBool("Attack", false);
            }
        }
    }

    private void Attack(Nightmare.YodaHealth enemyHealth)
    {
        if (enemyHealth != null && !enemyHealth.IsDead())
        {
            enemyHealth.TakeDamage(attackDamage, transform.position);
            animator.SetBool("Attack", true);
            lastAttackTime = Time.time;
        }
    }
}
