using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecoveryCollidable : PowerUpCollidable
{
    [SerializeField] private float healPercentage = 20;
    protected override void CollideEnter()
    {
        base.CollideEnter();
        PlayerStatistic.INSTANCE.addOrbsCollected();
        HealthSystem.Instance.HealDamagePercent(healPercentage);
        Destroy(gameObject);
    }
}
