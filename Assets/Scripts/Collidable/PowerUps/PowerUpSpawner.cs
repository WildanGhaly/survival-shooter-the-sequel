using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [System.Serializable]
    public class OrbProbability
    {
        public GameObject orbPrefab;
        public float probability;
    }

    public List<OrbProbability> orbsWithProbabilities;
    [SerializeField] private float notSpawnOrb = 0f;

    public void TrySpawnOrb()
    {
        float total = 0;
        foreach (var orb in orbsWithProbabilities)
        {
            total += orb.probability;
        }
        total += notSpawnOrb;
        float randomPoint = Random.Range(0, total);

        foreach (var orb in orbsWithProbabilities)
        {
            if (randomPoint < orb.probability)
            {
                Instantiate(orb.orbPrefab, transform.position, Quaternion.identity);
                return;
            }
            randomPoint -= orb.probability;
        }
    }
}
