using UnityEngine;
using System.Collections;
using Nightmare;
public class StatueHealth : EnemyHealth
{
    protected override void Death()
    {
        Debug.Log("DESTROYED STATUE");

        GetComponent<Animator>().SetBool("isDestroyed", true);
    }

    protected override void OnEnable()
    {
        currentHealth = startingHealth;
    }
}