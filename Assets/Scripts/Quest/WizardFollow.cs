using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardFollow : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Transform player;

    [SerializeField] private float spawningFrequency = 10f;
    [SerializeField] private float shootingFrequency = 10f;
    [SerializeField] private float firstActive = 3f;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private GameObject staff;

    [SerializeField] private int bulletPerAttack = 10;
    [SerializeField] private float maxAngle = 5f;

    [SerializeField] private float shootRange = 30f;
    [SerializeField] private int shootDamage = 10;

    [SerializeField] private AudioSource boomLaser;

    private float enemySpeed;
    private float enemyStoppingDistance;

    private bool isStarted;

    private void Awake()
    {
        enemySpeed = enemy.speed;
        enemyStoppingDistance = enemy.stoppingDistance;
        staff.GetComponent<LineRenderer>().positionCount = bulletPerAttack * 2;
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

        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle03") && !isStarted)
        {
            isStarted = true;
            StartCoroutine(StartAI());
        }

        if (IsWalking() && !enemyAnimator.GetBool("Attacking") && !enemyAnimator.GetBool("Summoning") && isStarted)
        {
            enemy.isStopped = false;
            enemy.speed = enemySpeed;
        }
        else
        {
            enemy.isStopped = true;
        }
    }


    private bool IsWalking()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        bool shouldWalk = distance > enemyStoppingDistance;
        enemyAnimator.SetBool("Walking", shouldWalk);
        return shouldWalk;
    }

    IEnumerator StartAI()
    {
        yield return new WaitForSeconds(firstActive);
        StartCoroutine(ShootAndSpawnPhase());
    }

    IEnumerator ShootAndSpawnPhase()
    {
        yield return new WaitForSeconds(shootingFrequency);
        enemyAnimator.SetBool("Attacking", true);
        StartCoroutine(Attacking());

        yield return new WaitForSeconds(shootingFrequency);
        enemyAnimator.SetBool("Attacking", true);
        StartCoroutine(Attacking());

        yield return new WaitForSeconds(spawningFrequency);
        enemyAnimator.SetBool("Summoning", true);
        StartCoroutine(Summoning());

        StartCoroutine(ShootAndSpawnPhase());
    }

    IEnumerator Summoning()
    {
        yield return new WaitForSeconds(3);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        enemyAnimator.SetBool("Summoning", false);
    }

    IEnumerator Attacking()
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
        enemyAnimator.SetBool("Attacking", false);
    }

    private void OnDisable()
    {
        GetComponent<BossKilledScene>().enabled = true;
        enemyAnimator.SetBool("Arise", false);
        enemyAnimator.SetBool("Attacking", false);
        enemyAnimator.SetBool("Summoning", false);
        StopAllCoroutines();
    }

}
