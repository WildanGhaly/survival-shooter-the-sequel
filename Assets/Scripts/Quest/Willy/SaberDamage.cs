using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberDamage : Collidable
{
    private Nightmare.PlayerHealth playerHealth;
    [SerializeField] private float damagePerHit = 25f;

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
