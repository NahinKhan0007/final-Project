using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable] //Will show up in inspector
    public class Wave
    {
        public Enemy[] enemies; //Array for enemies
        public int count; //dictates how many enemies will be per wave
        public float timeBetweenSpawns;//time before next spawn begins
    }

    public Wave[] waves; //array of waves
    public Transform[] spawnPoints; //Location where enemies will spawn
    public float timeBetweenWaves; //dictates frequency of waves

    private Wave currentWave; //points at current wave
    private int currentWaveIndex; //points at the current wave index
    private Transform player;

    private bool finishedSpawning;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Finding player position
        StartCoroutine(StartNextWave(currentWaveIndex)); //Starting first wave

    }

    IEnumerator StartNextWave(int index) // Will wait for X amount of seconds based on "timeBetweenWaves" value
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index) 
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null) //Player alive check
            {
                yield break;//Player dead, break
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)]; //Random enemy from Enemies index
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)]; //Random spawn point for the enemy
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation); //Spawner Instantiated

    
            //Checking if All waves have finished SPawning
            if (i == currentWave.count - 1)
            {
                finishedSpawning = true;
            } else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns); //Waiting  for timeBetweenSpawns
        }
    }

    private void Update()
    {
        //If current waves have finished spawning and there are no more enemies in the scene
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            } else
            {
                Debug.Log("Game FInished");
            }
        }
    }

}
