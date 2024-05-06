using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedCollidable : PowerUpCollidable
{
    [SerializeField] private float percentage = 20f;
    [SerializeField] private float duration = 15f;
    protected override void CollideEnter()
    {
        base.CollideEnter();
        PlayerPowerUp.Instance.IncreaseSpeedPowerUp(duration, percentage / 100f);
        Destroy(gameObject);
    }
}
