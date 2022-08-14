using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Waves Configuration for Enemies Spawn")]
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves()); //coroutine
    }

        public WaveConfigSO GetCurrentWave()
    {
        return currentWave;   
    }

    IEnumerator SpawnEnemyWaves()
    {
        foreach(WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
            for(int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
            Instantiate(currentWave.GetEnemyPrefab(i), 
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity, //Quaternion.identity cause no rotation to our object
                    transform); // to spawn enemies under "EnemySpawner" GameObject
            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime()); //coroutine to have time between Enemies spawn
            }
            yield return new WaitForSeconds(timeBetweenWaves); //coroutine to have time between Wave spawn
        }
        
    }
}