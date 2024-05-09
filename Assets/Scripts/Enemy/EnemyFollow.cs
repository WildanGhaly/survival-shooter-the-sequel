using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] protected float damageMultiplier = 1f;
    public virtual void AddDamageMultiplier(float value)
    {
        damageMultiplier += value;
    }
}
