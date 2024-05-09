using UnityEngine;
using System.Collections.Generic;

namespace Nightmare
{
    /// <summary>
    /// Inherit dari EnemyManager
    /// tapi mekanisme spawn berbeda dengan EnemyManager
    /// dimana musuh datang dengan variasi jenis musuh
    /// </summary>
    public class VariableEnemyManager : EnemyManager
    {
        public List<GameObject> enemies;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Spawn ()
        {
            // If the player has no health left or the queue is empty...
            if(HealthSystem.Instance.hitPoint <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Find a random index between zero and one less than the choices of enemies.
            enemy = enemies[Random.Range(0, enemies.Count)];

            // Create an instance of the enemy prefab at the selected spawn point's position and rotation.
            Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}