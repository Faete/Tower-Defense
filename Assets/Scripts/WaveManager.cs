using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] int lowestLevel;
    [SerializeField] int highestLevel;

    List<Enemy> enemies = new List<Enemy>();
    bool lastWaveSpawned = false;
    bool waveClearMessageNotSent = true;

    void Start(){
        StartCoroutine(SpawnWaves());
    }

    void Update(){
        enemies.RemoveAll(x => x == null);
        if(waveClearMessageNotSent && lastWaveSpawned && enemies.Count == 0){
            waveClearMessageNotSent = false;
            Tower[] towers = UnityEngine.Object.FindObjectsOfType<Tower>();
            InventoryManager inventoryManager = UnityEngine.Object.FindObjectOfType<InventoryManager>();
            foreach(Tower tower in towers) inventoryManager.critters.Add(tower.Recall());
            inventoryManager.Save();
        }
    }

    void SpawnCritter(){
        int randomIndex = Random.Range(0, critters.Count);
        Critter critter = Instantiate(critters[randomIndex]);
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        critter.level = Random.Range(lowestLevel, highestLevel + 1);
        enemy.path = path;
        enemy.critter = critter;
        enemies.Add(enemy);
    }

    IEnumerator SpawnWaves(){
        for(int i = 0; i < numWaves; ++i){
            for(int j = 0; j < crittersPerWave; ++j){
                SpawnCritter();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        lastWaveSpawned = true;
    }
}
