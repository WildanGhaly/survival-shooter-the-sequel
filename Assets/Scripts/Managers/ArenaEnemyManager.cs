using UnityEngine;
using System;
using Nightmare;

namespace Nightmare
{
    public class ArenaEnemyManager : VariableEnemyManager
    {
        public int targetSpawned = 50;
        protected override void Spawn()
        {
            if (spawned < targetSpawned)
            {
                base.Spawn();
                spawned++;
            }
            Debug.Log("Enemy is all eliminated!");
        }
    }
}

