using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] protected float damageMultiplier = 1f;

    protected virtual void Awake()
    {
        // TODO
        // damageMultiplier = GameManager.multiplier;
    }

    public virtual void AddDamageMultiplier(float value)
    {
        damageMultiplier += value;
    }
}
