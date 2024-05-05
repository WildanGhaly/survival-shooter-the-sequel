using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RajaFollow : MonoBehaviour
{
    private NavMeshAgent enemy;
    private Animator enemyAnimator;

    private Transform player;

    private float enemyStoppingDistance;
    [SerializeField] private float startChase = 1.5f;
    [SerializeField] private float defendTime = 5f;
    [SerializeField] private float attackFrequency = 10f;
    [SerializeField] private float defendFrequency = 15f;
    [SerializeField] private float runFrequency = 30f;

    [SerializeField] private float maxRunTime = 5f;
    [SerializeField] private float closestRunTarget = 10f;

    [SerializeField] private float closeRangeTrigger = 5f;
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private float shootDamage = 10f;
    [SerializeField] private int bulletPerAttack = 10;
    [SerializeField] private float maxAngle = 5f;

    [SerializeField] private Animator saber;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject staff;

    [SerializeField] private AudioSource boomLaser;

    private bool mutexLockGeneral = false;
    private bool mutexLockAttack = false;
    private bool mutexLockDefend = false;
    private bool mutexLockRun = false;

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

    IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(startChase);
        StartCoroutine(RunFrequencyStart());
        StartCoroutine(AttackFrequencyStart());
        StartCoroutine(DefendFrequencyStart());
        Started();
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
        
        if (HealthSystem.Instance.isDeath)
        {
            enemy.isStopped = true;
            return;
        }

        if (IsRun() && Vector3.Distance(transform.position, player.position) <= closestRunTarget)
        {
            StopRun();
            mutexLockGeneral = false;
        }

        if (!mutexLockGeneral)
        {
            if (!mutexLockRun)
            {
                mutexLockRun = true;
                StartCoroutine(Run());
            }
            else if (CloseRangeShouldAttack())
            {
                StopRun();
                StartCoroutine(CloseRange());
            }
            else if (!mutexLockAttack)
            {
                StopRun();
                mutexLockAttack = true;
                StartCoroutine(Attack());
            }
            else if (!mutexLockDefend)
            {
                StopRun();
                mutexLockDefend = true;
                StartCoroutine(Defend());
            }
            else
            {
                Debug.Log("Emang bisa kesini?");
            }
        }
        
    }

    private bool IsDeath()
    {
        return enemyAnimator.GetBool("Death");
    }

    private bool IsRun()
    {
        return enemyAnimator.GetBool("Run");
    }

    private bool IsAttack()
    {
        return enemyAnimator.GetBool("Attack");
    }
    private bool IsAttacking()
    {
        return enemyAnimator.GetBool("Attacking");
    }

    private bool IsCloseRange()
    {
        return enemyAnimator.GetBool("CloseRange");
    }

    private bool IsDefend()
    {
        return enemyAnimator.GetBool("Defend");
    }

    private bool IsDefending()
    {
        return enemyAnimator.GetBool("Defending");
    }

    private bool IsStarted()
    {
        return enemyAnimator.GetBool("Started");
    }

    private void Death()
    {
        enemyAnimator.SetBool("Death", true);
    }

    private IEnumerator Run()
    {
        mutexLockGeneral = true;
        enemy.isStopped = false;
        enemyAnimator.SetBool("Run", true);
        yield return new WaitForSeconds(maxRunTime);
        enemy.isStopped = true;
        if (IsRun())
        {
            enemyAnimator.SetBool("Run", false);
            mutexLockGeneral = false;
        }
    }

    private void StopRun()
    {
        enemy.isStopped = true;
        enemyAnimator.SetBool("Run", false);
    }

    private bool CloseRangeShouldAttack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= closeRangeTrigger;
    }

    private IEnumerator Attack()
    {
        mutexLockGeneral = true;
        enemyAnimator.SetBool("Attack", true);
        StartCoroutine(SingleAttack());
        yield return new WaitForSeconds(3);
        enemyAnimator.SetBool("Attacking", true);
        yield return new WaitForSeconds(3);
        enemyAnimator.SetBool("Attack", false);
        enemyAnimator.SetBool("Attacking", false);
        mutexLockGeneral = false;
    }

    private IEnumerator SingleAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.6f);
            staff.GetComponent<LineRenderer>().enabled = true;
            staff.GetComponent<LineRenderer>().positionCount = 2;
            Vector3 startPoint = staff.transform.position;
            Vector3 baseDirection = player.position + Vector3.up * 0.5f - startPoint;
            Vector3 targetPoint = startPoint * shootRange;
            if (Physics.Raycast(startPoint, baseDirection, out RaycastHit hit, shootRange))
            {
                Nightmare.PlayerHealth playerHealth = hit.collider.GetComponent<Nightmare.PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(shootDamage);
                }
                targetPoint = hit.point;
            }
            staff.GetComponent<LineRenderer>().SetPosition(0, startPoint);
            staff.GetComponent<LineRenderer>().SetPosition(1, targetPoint);
            yield return new WaitForSeconds(0.05f);
            staff.GetComponent<LineRenderer>().enabled = false;
        }

        StartCoroutine(ChargeAttack());
    }

    private IEnumerator ChargeAttack()
    {
        staff.GetComponent<Light>().enabled = true;
        staff.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.2f);
        staff.GetComponent<LineRenderer>().enabled = true;

        Vector3 startPoint = staff.transform.position;
        Vector3 baseDirection = player.position + Vector3.up * 0.5f - startPoint;
        baseDirection.Normalize();

        staff.GetComponent<LineRenderer>().positionCount = 2 * bulletPerAttack;

        for (int i = 0; i < bulletPerAttack; i++)
        {
            float angle = Random.Range(-maxAngle, maxAngle);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 shotDirection = rotation * baseDirection;

            Vector3 targetPoint = startPoint + shotDirection * shootRange;

            if (Physics.Raycast(startPoint, shotDirection, out RaycastHit hit, shootRange))
            {
                Nightmare.PlayerHealth playerHealth = hit.collider.GetComponent<Nightmare.PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(shootDamage);
                }
                targetPoint = hit.point;
            }

            staff.GetComponent<LineRenderer>().SetPosition(i * 2, startPoint);
            staff.GetComponent<LineRenderer>().SetPosition(i * 2 + 1, targetPoint);
        }
        boomLaser.Play();
        yield return new WaitForSeconds(0.05f);
        staff.GetComponent<LineRenderer>().enabled = false;
        staff.GetComponent<Light>().enabled = false;
    }

    private IEnumerator CloseRange()
    {
        mutexLockGeneral = true;
        enemyAnimator.SetBool("CloseRange", true);
        saber.SetBool("Activate", true);
        yield return new WaitForSeconds(0.5f);
        saber.SetBool("Activate", false);
        enemyAnimator.SetBool("CloseRange", false);
        mutexLockGeneral = false;
    }

    private IEnumerator Defend()
    {
        mutexLockGeneral = true;
        shield.SetActive(true);
        enemyAnimator.SetBool("Defend", true);
        yield return new WaitForSeconds(0.5f); // delay sebelum defend lanjutan
        enemyAnimator.SetBool("Defending", true);
        yield return new WaitForSeconds(defendTime);
        enemyAnimator.SetBool("Defend", false);
        enemyAnimator.SetBool("Defending", false);
        shield.SetActive(false);
        mutexLockGeneral = false;
    }

    private IEnumerator AttackFrequencyStart()
    {
        while (!IsDeath())
        {
            yield return new WaitForSeconds(attackFrequency);
            mutexLockAttack = false;
        }
    }

    private IEnumerator DefendFrequencyStart()
    {
        while (!IsDeath())
        {
            yield return new WaitForSeconds(defendFrequency);
            mutexLockDefend = false;
        }
    }

    private IEnumerator RunFrequencyStart()
    {
        while (!IsDeath())
        {
            yield return new WaitForSeconds(runFrequency);
            mutexLockRun = false;
        }
    }

    private void Started()
    {
        enemyAnimator.SetBool("Started", true);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
