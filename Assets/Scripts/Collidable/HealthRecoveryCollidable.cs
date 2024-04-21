using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecoveryCollidable : Collidable
{
    protected override void CollideEnter()
    {
        base.CollideEnter();
        HealthSystem.Instance.FullHealDamage();
        Destroy(gameObject);
    }
}
