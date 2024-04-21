using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedCollidable : Collidable
{
    [SerializeField] private float normalSpeed = 9f;
    protected override void CollideEnter()
    {
        base.CollideEnter();
        PlayerPowerUp.Instance.IncreaseSpeedPowerUp(normalSpeed);
        Destroy(gameObject);
    }
}
