using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRecoveryCollidable : PowerUpCollidable
{
    protected override void CollideEnter()
    {
        base.CollideEnter();
        PlayerStatistic.INSTANCE.addOrbsCollected();
        HealthSystem.Instance.FullRecoverMana();
        Destroy(gameObject);
    }
}
