using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberDamage : Collidable
{
    private Nightmare.PlayerHealth playerHealth;
    private float damagePerHit;

    public void SetDamagePerHit(float damage)
    {
        damagePerHit = damage;
    }

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Nightmare.PlayerHealth>();
    }

    protected override void CollideEnter()
    {
        base.CollideEnter();
        playerHealth.TakeDamage(damagePerHit);
    }
}
