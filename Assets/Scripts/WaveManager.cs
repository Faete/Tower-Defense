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
    [SerializeField] Critter boss;
    [SerializeField] int numWaves;
    [SerializeField] int crittersPerWave;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] int lowestLevel;
    [SerializeField] int highestLevel;
    public int levelId;
    [SerializeField] GameObject critterPanel;
    [SerializeField] GameObject levelCompletePanel;

    List<Enemy> enemies = new List<Enemy>();
    bool lastWaveSpawned = false;
    bool waveClearMessageNotSent = true;

    public int catchersPrize;

    void Start(){
        Time.timeScale = 1f;
        StartCoroutine(SpawnWaves());
    }

    void Update(){
        enemies.RemoveAll(x => x == null);
        if(waveClearMessageNotSent && lastWaveSpawned && enemies.Count == 0){
            // This should really be handled elsewhere using Unity's Event System
            // Oh well...
            waveClearMessageNotSent = false;
            Builder builder = FindObjectOfType<Builder>();
            if(builder != null) Destroy(builder.gameObject);
            Tower[] towers = FindObjectsOfType<Tower>();
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            foreach(Tower tower in towers) tower.Recall();
            critterPanel.gameObject.SetActive(false);
            levelCompletePanel.gameObject.SetActive(true);
            if(inventoryManager.savedata.level <= levelId){
                inventoryManager.catchers += catchersPrize;
                inventoryManager.savedata.level++;
                levelCompletePanel.transform.GetChild(1).gameObject.SetActive(true);
            }
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

    void SpawnBoss(){
        Critter critter = Instantiate(boss);
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.path = path;
        enemy.critter = critter;
        enemy.gameObject.tag = "Boss";
        enemies.Add(enemy);
    }

    IEnumerator SpawnWaves(){
        yield return new WaitForSeconds(timeBetweenWaves);
        for(int i = 0; i < numWaves; ++i){
            for(int j = 0; j < crittersPerWave; ++j){
                SpawnCritter();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        if(boss != null) SpawnBoss();
        lastWaveSpawned = true;
    }
}
