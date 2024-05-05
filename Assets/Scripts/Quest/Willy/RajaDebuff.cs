using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaDebuff : JenderalDebuff
{
    [SerializeField] private float speedDebuff = 20f, attackDebuff = 50f;

    bool isDebuff;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void CollideEnter()
    {
        if (!isDebuff)
        {
            isDebuff = true;
            base.CollideEnter();
            BaseInstance.Instance.AddMultiplierGunDamage(-attackDebuff / 100);
            BaseInstance.Instance.AddMultiplierSpeed(-speedDebuff / 100);
        }
    }

    protected override void CollideStay()
    {
        base.CollideStay();
    }

    protected override void CollideExit()
    {
        if (isDebuff)
        {
            isDebuff = false;
            base.CollideExit();
            BaseInstance.Instance.AddMultiplierGunDamage(attackDebuff / 100);
            BaseInstance.Instance.AddMultiplierSpeed(speedDebuff / 100);
        }
    }
}
