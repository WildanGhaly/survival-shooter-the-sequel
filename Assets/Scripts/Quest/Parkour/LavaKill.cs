using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaKill : MonoBehaviour
{
    // in trigger exit because to make sure object pass through completely
    // and also opens level design possibility
    private void OnTriggerEnter(Collider other)
    {
        // FIXME HealthSystem for player is not in Player object but in PlayerUI object

        // Kenali dulu siapa yang jatuh, dealdamage sebanyak maxhealth/startinghealth
        if (other.gameObject.name.Contains("Player"))
        {
            Nightmare.PlayerHealth victim = other.gameObject.GetComponent<Nightmare.PlayerHealth>();
            victim.TakeDamage(GameObject.Find("PlayerUI").GetComponentInChildren<HealthSystem>().maxHitPoint);
            return;
        } else if (other.gameObject.tag.Contains("FinalBossEnemy"))
        {
            // TODO not tested yet, but should work?
            Nightmare.EnemyHealth victim = other.gameObject.GetComponent<Nightmare.EnemyHealth>();
            victim.TakeDamage(victim.startingHealth, transform.position);
            return;
        }
        
    }
}
