using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentIncreaseDamageCollidable : PowerUpCollidable
{
    protected override void CollideEnter()
    {
        base.CollideEnter();
        GameManager.INSTANCE.AddUltimate();
        Destroy(gameObject);
    }
}
