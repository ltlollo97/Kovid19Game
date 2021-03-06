﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public GameObject spawnEffect;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count; // number of enemies spawned in a wave
        public float rate;
    }
    
    public Transform[] spawnPoints;

    public Wave[] waves;
    [Header("Quanto tempo aspetto per la prima ondata da spawnare")]
    public int firstWaveSpawn;
    private int nextWave = 0;
    private int limitSound = 15;
    [Header("Quanto tempo passa tra due ondate successive")]
    public float timeBetweenWaves;
    private float waveCountdown;
    private float searchCountdown;
    protected Player player;
    private int remainingWaves;
    public SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        waveCountdown = firstWaveSpawn; // first enemy spawned after 1 sec
        remainingWaves = waves.Length;
    }

    private void Update()
    {
 
        if (remainingWaves > 0)
        {
            if (state == SpawnState.WAITING)
            {

                if (waveCountdown <= 0)
                    WaveCompleted();
                else
                    waveCountdown -= Time.deltaTime;

                /*if (!EnemyisAlive())  // spawn new wave only if all enemies have been killed
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }*/
            }


            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            remainingWaves -= 1;
            Debug.Log("All waves completed");
        }
        else
        {
            nextWave++;
            remainingWaves -= 1;
        }


    }

    bool EnemyisAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }

        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        state = SpawnState.WAITING; // wait the player kills all enemies

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
        if (spawnPoints.Length == 0)
        {
            Debug.Log("error: no spawn points reference");
        }
        Instantiate(spawnEffect, sp.position, sp.rotation);
        Instantiate(enemy, sp.position, sp.rotation);
    }

    public int EnemyNumber()
    {
        int enemy_count = 0;

        foreach (Wave wave in waves)
        {
            enemy_count += wave.count;
        }

        return enemy_count;
    }
}