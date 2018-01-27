﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnerController : MonoBehaviour {

    public float Z_areaDistance = 100;
    public float X_areaDistance = 10;
    public float delayBeforeFirstRespawn = 5;
    public float minRespawnDelay = 20;
    public float maxRespawnDelay = 30;
    public float respawnRateMultiplayer = 0.9f;
    public GameObject enemyPrefab;
    public GameObject enemyTarget;
    //public float enemyTargetWidth = 15;

    private Transform enemyTargetTransform;
    private bool isEnemyReady;
    private float respawnTimer;
    private Vector3 respawnPosition;

    // Use this for initialization
    void Start () {
        isEnemyReady = false;
        enemyTargetTransform = enemyTarget.transform;
    }
	
	// Update is called once per frame
	void Update () {
        //first wave timer
		if (delayBeforeFirstRespawn > 0)
        {
            delayBeforeFirstRespawn -= Time.deltaTime;
        }
        //respawning start
        else if (!isEnemyReady)
        {
            respawnPosition = new Vector3(UnityEngine.Random.RandomRange(-X_areaDistance, X_areaDistance), 0, UnityEngine.Random.RandomRange(-Z_areaDistance, Z_areaDistance));
            respawnTimer = UnityEngine.Random.RandomRange(minRespawnDelay, maxRespawnDelay);
            isEnemyReady = true;
        }
        //count to respwan
        else if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
        }
        //respawn
        else
        {

            var newEnemy = Instantiate(enemyPrefab, transform.position + respawnPosition, new Quaternion(0,0,0,0));
            //var targetRandomWidthPoint = new Vector3(UnityEngine.Random.RandomRange(-enemyTargetWidth, enemyTargetWidth), 0, 0);
            //enemyTargetTransform.position += targetRandomWidthPoint;

            newEnemy.GetComponent<EnemyController>().target = enemyTargetTransform;

            //enemyTargetTransform.position -= targetRandomWidthPoint;

            minRespawnDelay *= respawnRateMultiplayer;
            maxRespawnDelay *= respawnRateMultiplayer;
            isEnemyReady = false;
        }
	}
}