using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageCollidable : PowerUpCollidable
{
    [SerializeField] private int multiplierDamage = 2;
    [SerializeField] private float duration = 15f;

    protected override void CollideEnter()
    {
        base.CollideEnter();
        PlayerPowerUp.Instance.IncreaseDamagePowerUp(duration, multiplierDamage);
        Destroy(gameObject);
    }
}
