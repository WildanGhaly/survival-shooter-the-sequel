using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MazeMonster : MonoBehaviour
{
    private Transform player;
    private Nightmare.PlayerHealth playerHealth;
    private NavMeshAgent navMesh;
    private bool isAttack;
    private GameObject attack;
    private GameObject walk;
    private AudioSource attackAudioSource;
    private AudioSource walkAudioSource;

    [SerializeField] private float damagePerHit = 40f;
    private float damage;

    void SetUpDamage()
    {
        damage = damagePerHit * GameManager.INSTANCE.multiplier;
    }

    private void Awake()
    {
        SetUpDamage();
        player = GameObject.FindWithTag("Player").transform;
        navMesh = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<Nightmare.PlayerHealth>();
        attack = transform.Find("Attack").gameObject;
        walk = transform.Find("Walk").gameObject;
        if (attack != null)
            attackAudioSource = attack.GetComponent<AudioSource>();
        if (walk != null)
            walkAudioSource = walk.GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartWalking());
    }

    // Update is called once per frame
    void Update()
    {
        // DO NOTHING
    }

    private void FixedUpdate()
    {
        navMesh.SetDestination(player.position);
        if (GetComponent<Animator>().GetBool("isNotIdle") && !HealthSystem.Instance.isDeath)
        {
            navMesh.isStopped = false;
            if (Vector3.Distance(player.position, transform.position) < 3)
            {
                GetComponent<Animator>().SetBool("isWalking", false);
                GetComponent<Animator>().SetBool("isAttacking", true);
                if (!isAttack)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("isAttacking", false);
                GetComponent<Animator>().SetBool("isWalking", true);
            }
        }
        else if (!HealthSystem.Instance.isDeath)
        {
            navMesh.isStopped = true;
        }
        else
        {
            StopMovement();
        }
    }
    IEnumerator Attack()
    {
        isAttack = true;
        attackAudioSource.Play();
        yield return new WaitForSeconds(1);
        playerHealth.TakeDamage(damage);
        isAttack = false;
    }
    IEnumerator StartWalking()
    {
        yield return new WaitForSeconds(15);
        walkAudioSource.Play();
        GetComponent<Animator>().SetBool("isNotIdle", true);
    }
    public void StopMovement()
    {
        navMesh.isStopped = true;
        GetComponent<Animator>().SetBool("isPlayerDeath", true);
        GetComponent<Animator>().SetBool("isNotIdle", false);
        walkAudioSource.Stop();
    }
}
