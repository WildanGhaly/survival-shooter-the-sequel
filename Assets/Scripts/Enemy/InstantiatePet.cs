using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePet : MonoBehaviour
{
    [SerializeField] private GameObject pet;
    [SerializeField] private int numberOfPet = 1;
    [SerializeField] private float spawnRadius = 5.0f;

    void Start()
    {
        for (int i = 0; i < numberOfPet; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0;
            Vector3 spawnPosition = transform.position + randomPosition;
            
            GameObject enemyPet = Instantiate(pet, spawnPosition, Quaternion.identity);
            enemyPet.GetComponent<EnemyPet>().enemy = gameObject;
            enemyPet.GetComponent<EnemyPet>().enabled = true;
        }
    }
}
