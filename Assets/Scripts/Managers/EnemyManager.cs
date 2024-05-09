﻿using UnityEngine;

namespace Nightmare
{
    public class EnemyManager : PausibleObject
    {
        private PlayerHealth playerHealth;
        public GameObject enemy;
        public float spawnTime = 3f;
        public Transform[] spawnPoints;

        private float timer;
        protected int spawned = 0;

        protected virtual void Start ()
        {
            timer = spawnTime;
        }

        void OnEnable()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            StartPausible();
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
            if (timer <= 0f)
            {
                Spawn();
                timer = spawnTime;
            }
        }

        protected virtual void Spawn ()
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