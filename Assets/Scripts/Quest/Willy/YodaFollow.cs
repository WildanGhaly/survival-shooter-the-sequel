using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class YodaFollow : EnemyFollow
{
    private NavMeshAgent enemy;
    private Animator enemyAnimator;

    private Transform player;

    private float enemyStoppingDistance;
    [SerializeField] private float startChase = 1.5f;
    [SerializeField] private float damagePerHit = 30f;
    [SerializeField] private SaberDamage saber;

    public float damageMultiplier = 1f;

    public override void AddDamageMultiplier(float value)
    {
        damageMultiplier += value;
        SetSaberDamage();
    }

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").transform;
        enemyStoppingDistance = enemy.stoppingDistance;
    }

    private void Start()
    {
        StartCoroutine(StartRunning());
    }

    private void SetSaberDamage()
    {
        saber.SetDamagePerHit(damagePerHit * damageMultiplier);
    }

    IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(startChase);
        enemyAnimator.SetBool("Started", true);
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * 5);
        }

        enemy.SetDestination(player.position);
        if (!IsAttacking() && !enemyAnimator.GetBool("Die") && !enemyAnimator.GetBool("Attack") && enemyAnimator.GetBool("Started")  && !HealthSystem.Instance.isDeath)
        {
            enemy.isStopped = false;
            enemyAnimator.SetBool("Run", true);
        }
        else if (HealthSystem.Instance.isDeath)
        {
            enemyAnimator.SetBool("Idle", true);
            enemy.isStopped = true;
        }
        else
        {
            enemy.isStopped = true;
        }
    }


    private bool IsAttacking()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        bool shouldAttack = distance < enemyStoppingDistance;
        enemyAnimator.SetBool("Attack", shouldAttack);
        enemyAnimator.SetBool("Run", !shouldAttack);
        return shouldAttack;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
