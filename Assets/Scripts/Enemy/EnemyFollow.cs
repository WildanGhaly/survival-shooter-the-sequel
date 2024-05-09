using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] protected float damageMultiplier = 1f;

    protected virtual void Awake()
    {
        damageMultiplier = GameManager.INSTANCE.multiplier;
    }

    public virtual void AddDamageMultiplier(float value)
    {
        damageMultiplier += value;
    }

    public virtual void SetDamageMultiplier(float value)
    {
        damageMultiplier = value;
    }

    public virtual void ResetDamageMultiplier()
    {
        damageMultiplier = GameManager.INSTANCE.multiplier;
    }
}
