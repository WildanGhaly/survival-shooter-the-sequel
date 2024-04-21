using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageCollidable : Collidable
{
    [SerializeField] private int multiplierDamage = 2;

    protected override void CollideEnter()
    {
        base.CollideEnter();
        PlayerPowerUp.Instance.IncreaseDamagePowerUp(multiplierDamage);
        Destroy(gameObject);
    }
}
