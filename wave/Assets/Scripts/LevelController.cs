﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPositions = null;
    [SerializeField]
    private GameObject enemy = null;
    [SerializeField]
    private float spawnDelay = 0.0f;
    [SerializeField]
    private int maxEnemies = 5;
    private int spawnIndex = 0;
    private int currentNumEnemies;
    private float spawnTime = 0.0f;
    private float nextSpawn = 0.5f;

    // Update is called once per frame
    void Update()
    {
        spawn();
    }

    private void spawn()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > nextSpawn && currentNumEnemies < maxEnemies)
        {
            nextSpawn = spawnTime + spawnDelay;

            Transform t = getSpawnTransform();
            Instantiate(enemy, t.position, t.rotation);
            currentNumEnemies++;

            nextSpawn -= spawnTime;
            spawnTime = 0.0f;
        }
    }

    private Transform getSpawnTransform()
    {
        if (++spawnIndex >= spawnPositions.Length)
        {
            spawnIndex = 0;
        }
        return spawnPositions[spawnIndex];
    }
}
