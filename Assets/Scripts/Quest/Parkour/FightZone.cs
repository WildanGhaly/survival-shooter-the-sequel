using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FightZone : MonoBehaviour
{
    public Nightmare.ArenaEnemyManager enemyManager;
    public List<PlayableDirector> cutscene;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Fight Time!");
        if (enemyManager.isDoneSpawning && !other.gameObject.tag.Contains("Player"))
        {
            return;
        }
        cutscene[0].stopped += ctx => { enemyManager.enabled = true; };
        cutscene[0].Play();        
    }

    // Scene continues if theres no more enemies in FightZone
    public bool HasEnemyAlive()
    {
        GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in enemiesLeft)
        {
            if (item.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!HasEnemyAlive() && enemyManager.isDoneSpawning)
        {
            cutscene[1].stopped += ctx => { gameObject.GetComponent<MeshCollider>().enabled = false; };
            cutscene[1].Play();
        }
    }
}
