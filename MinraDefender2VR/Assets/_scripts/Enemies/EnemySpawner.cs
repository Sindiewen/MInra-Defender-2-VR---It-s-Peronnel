using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public GameObject[] EnemiesToSpawn;
    public int enemiesToSpawnPerWave;
    public float timeBetweenWaves;


    private void Start()
    {
        StartCoroutine(spawnWaves());
    }

    IEnumerator spawnWaves()
    {
        while(true)
        {   
            for (int i = 0; i < enemiesToSpawnPerWave; ++i)
            {
                spawnEnemy();
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            enemiesToSpawnPerWave += 1;
        }
    }

    public void spawnEnemy()
    {
        // get random number
        // spawn enemy at spawn point based on random number chosen
        
        int num = Random.Range(0, spawnPoints.Length);

        GameObject clone = Instantiate(EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Length)], spawnPoints[num].transform.position, Quaternion.identity) as GameObject;
        clone.transform.parent = gameObject.transform;
    }
}
