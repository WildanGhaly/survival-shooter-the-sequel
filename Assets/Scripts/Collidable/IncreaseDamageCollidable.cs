using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageCollidable : Collidable
{
    protected override void CollideEnter()
    {
        BaseInstance.Instance.UpdateGunDamage(BaseInstance.Instance.gunDamage * 2);
        base.CollideEnter();
    }
}
