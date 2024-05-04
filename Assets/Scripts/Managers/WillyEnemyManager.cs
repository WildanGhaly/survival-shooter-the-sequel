using System.Collections;
using UnityEngine;

namespace Nightmare
{
    public class WillyEnemyManager : PausibleObject
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private float spawnTime = 3f;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int targetSpawn;
        [SerializeField] private int firstSpawnTime;

        void Awake()
        {
            StartCoroutine(StartSpawn());          
        }

        void OnEnable()
        {
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        private IEnumerator StartSpawn()
        {
            yield return new WaitWhile(() => isPaused);
            yield return new WaitForSeconds(firstSpawnTime);
            StartCoroutine(Spawn(spawnTime, targetSpawn));
        }

        private IEnumerator Spawn(float spawnTime, int targetSpawn)
        {
            for (int i = 0; i < targetSpawn; i++)
            {

                // If the player has no health left...
                if (HealthSystem.Instance.hitPoint <= 0f)
                {
                    // ... exit the function.

                }

                // Find a random index between zero and one less than the number of spawn points.
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.

                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}