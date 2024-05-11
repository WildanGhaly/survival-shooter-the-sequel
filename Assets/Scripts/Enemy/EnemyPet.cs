using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPet : MonoBehaviour
{

    [SerializeField] private float damageIncreasePercent = 20f;
    [SerializeField] private float radiusFromEnemy = 10f;
    [SerializeField] private float petSpeed = 5f;

    private GameObject player;
    private Transform pointA; // player
    private Transform pointB; // enemy
    private Vector3 pointC; // target
    private Transform pointD; // slime
    private Animator animate;
    private EnemyFollow enemyFollow;

    public GameObject enemy;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pointA = player.transform;
        pointB = enemy.transform;
        pointC = Vector3.zero;
        pointD = transform;
        animate = GetComponent<Animator>();
        enemyFollow = enemy.GetComponent<EnemyFollow>();
    }

    private void OnEnable()
    {
        enemy.GetComponent<EnemyFollow>().AddDamageMultiplier(damageIncreasePercent / 100f);
    }

    void Update()
    {
        if (pointB == null)
        {
            Stay();
            return;
        }
        GetPositionC(pointA, pointB);
        LookToC(pointC, pointD);
        float distanceToPlayer = Vector3.Distance(pointA.position, pointD.position);
        float distanceToEnemy = Vector3.Distance(pointB.position, pointD.position);
        
        if (distanceToEnemy < radiusFromEnemy && distanceToPlayer > radiusFromEnemy)
        {
            Stay();
        }
        else
        {
            Run();
        }
    }

    void GetPositionC(Transform pointA, Transform pointB)
    {
        float distanceToPlayer = Vector3.Distance(pointA.position, pointD.position);
        float distanceToEnemy = Vector3.Distance(pointB.position, pointD.position);

        if (distanceToEnemy < distanceToPlayer)
        {
            Vector3 direction = pointB.position - pointA.position;
            Vector3 normalizedDirection = direction.normalized;
            pointC = pointB.position + normalizedDirection * radiusFromEnemy;
        }
        else if (distanceToPlayer < radiusFromEnemy)
        {
            Vector3 direction = pointD.position - pointA.position;
            Vector3 normalizedDirection = direction.normalized;
            pointC = pointD.position + normalizedDirection * radiusFromEnemy;
        }
        else
        {
            Vector3 direction = pointA.position - pointD.position;
            Vector3 normalizedDirection = direction.normalized;
            pointC = pointA.position + normalizedDirection * radiusFromEnemy;
        }
    }

    void LookToC(Vector3 pointC, Transform pointD)
    {
        Vector3 direction = pointC - pointD.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion yRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        pointD.rotation = yRotation;
    }

    void Run()
    {
        animate.SetFloat("Speed", petSpeed);
    }
    
    void Stay()
    {
        animate.SetFloat("Speed", 0);
    }

    private void OnDisable()
    {
        enemyFollow.AddDamageMultiplier(-damageIncreasePercent / 100f);
    }
}
