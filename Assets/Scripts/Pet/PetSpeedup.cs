using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetSpeedup : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Animator animator;
    private Transform player;

    public float detectionRadius = 5.0f;
    public float sprintSpeedUpMultiplier = 20f;
    public float sprintRadius = 3.0f;
    private bool speedBoostActive = false;
    private float boostTimer = 0f;
    private float delayTimer = 0f;
    private const float boostDuration = 1f;
    private const float boostDelay = 3f;


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
        float distance = Vector3.Distance(navMesh.transform.position, player.position);
        if (distance > 3.0f)
        {
            navMesh.SetDestination(player.position);
        }
        else
        {
            navMesh.ResetPath();
            if (speedBoostActive)
            {
                boostTimer += Time.deltaTime;
                if (boostTimer >= boostDuration)
                {
                    BaseInstance.Instance.AddMultiplierSpeed(-sprintSpeedUpMultiplier / 100);
                    speedBoostActive = false;
                    boostTimer = 0f;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }

            if (distance <= sprintRadius && !speedBoostActive && delayTimer >= boostDelay)
            {
                BaseInstance.Instance.AddMultiplierSpeed(sprintSpeedUpMultiplier / 100);
                speedBoostActive = true;
                delayTimer = 0f;
            }
        }
    }
}
