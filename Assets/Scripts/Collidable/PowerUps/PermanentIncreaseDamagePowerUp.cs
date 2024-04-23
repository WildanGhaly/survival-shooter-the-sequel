using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentIncreaseDamageCollidable : PowerUpCollidable
{
    [SerializeField] private float multiplierDamage = 0.1f;

    protected override void CollideEnter()
    {
        base.CollideEnter();
        PlayerPowerUp.Instance.PermanentDamagePowerUp(multiplierDamage);
        Destroy(gameObject);
    }
}
