using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightZone : MonoBehaviour
{
    public Nightmare.EnemyManager enemyManager;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Fight Time!");
        enemyManager.enabled = true;
    }
}
