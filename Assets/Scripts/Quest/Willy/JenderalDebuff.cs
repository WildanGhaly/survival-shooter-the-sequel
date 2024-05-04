using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JenderalDebuff : Collidable
{
    [SerializeField] private float debuffRadius = 2f, damagePerSecond = 10f;

    private Nightmare.PlayerHealth playerHealth;

    void Awake()
    {
        transform.localScale = new Vector3(debuffRadius, 1, debuffRadius);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Nightmare.PlayerHealth>();
    }

    protected override void CollideStay()
    {
        base.CollideStay();
        playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
    }
}
