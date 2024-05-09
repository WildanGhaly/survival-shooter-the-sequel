using UnityEngine;
using System;
using Nightmare;

namespace Nightmare
{
    public class ArenaEnemyManager : VariableEnemyManager
    {
        public int targetSpawned = 50;
        public bool isDoneSpawning = false;
        protected override void Spawn()
        {
            if (spawned < targetSpawned)
            {
                base.Spawn();
                spawned++;
            } else
            {
                isDoneSpawning = true;
                Debug.Log("Enemy is all eliminated!");
                enabled = false;
            }
        }
    }
}

