using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRecoveryCollidable : PowerUpCollidable
{
    protected override void CollideEnter()
    {
        base.CollideEnter();
        HealthSystem.Instance.FullRecoverMana();
        Destroy(gameObject);
    }
}
