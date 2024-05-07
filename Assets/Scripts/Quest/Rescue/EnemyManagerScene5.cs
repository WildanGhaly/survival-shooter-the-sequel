using UnityEngine;

namespace Nightmare
{
    public class SpawnOnce : PausibleObject
    {
        public GameObject enemy;
        public float spawnTime = 3f;
        public Transform[] spawnPoints;

        private float timer;
        private bool isSpawned;

        void Start ()
        {
            timer = spawnTime;
            isSpawned = false;
        }

        void OnEnable()
        {
            if(HealthSystem.Instance.hitPoint <= 0f)
            {
                StartPausible();
            }
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update()
        {
            if (isPaused)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0f & !isSpawned)
            {
                Spawn();
                isSpawned = true;
            }
        }

        void Spawn ()
        {
            // If the player has no health left...
            if(HealthSystem.Instance.hitPoint <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            
            Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}