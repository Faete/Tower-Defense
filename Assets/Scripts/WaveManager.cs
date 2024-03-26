using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform path;

    [SerializeField] List<Critter> critters;
    [SerializeField] int numWaves;
    [SerializeField] int crittersPerWave;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float timeBetweenSpawns;

    void Start(){
        StartCoroutine(SpawnAllWaves());
    }

    void SpawnCritter(){
        int randomIndex = Random.Range(0, critters.Count);
        Critter critter = Instantiate(critters[randomIndex]);
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.path = path;
        enemy.critter = critter;
    }

    IEnumerator SpawnWave(){
        for(int i = 0; i < crittersPerWave; ++i){
            SpawnCritter();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    IEnumerator SpawnAllWaves(){
        for(int i = 0; i < numWaves; ++i){
            StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
