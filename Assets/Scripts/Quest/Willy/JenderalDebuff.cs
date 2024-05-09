using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JenderalDebuff : Collidable
{
    [SerializeField] private float debuffRadius = 2f, damagePerSecond = 10f;
    [SerializeField] private Transform playerTransform;

    public float multiplier = 1f;

    private Nightmare.PlayerHealth playerHealth;

    protected virtual void Awake()
    {
        transform.localScale = new Vector3(debuffRadius, 1, debuffRadius);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Nightmare.PlayerHealth>();
        
        multiplier = GameManager.INSTANCE.multiplier;
    }

    protected override void CollideEnter()
    {
        base.CollideEnter();
    }

    protected override void CollideStay()
    {
        base.CollideStay();
        playerHealth.TakeDamage(DamagePerSecond() * Time.deltaTime);
    }

    protected override void CollideExit()
    {
        base.CollideExit();
    }

    private float DamagePerSecond()
    {
        return damagePerSecond * multiplier;
    }
}
