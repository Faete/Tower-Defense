using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] List<GameObject> hearts;
    [SerializeField] GameObject critterPanel;
    [SerializeField] GameObject levelFailedPanel;
    int lives = 3;

    void Update()
    {
        if(lives == 0){
            Builder builder = FindObjectOfType<Builder>();
            if(builder != null) Destroy(builder.gameObject);
            Tower[] towers = FindObjectsOfType<Tower>();
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            foreach(Tower tower in towers) tower.Recall();
            inventoryManager.Save();           
            critterPanel.SetActive(false);
            levelFailedPanel.SetActive(true);

        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            lives -= 1;
            hearts[lives].SetActive(false);
        }
        if(other.CompareTag("Boss")){
            lives = 0;
            foreach(GameObject heart in hearts){
                heart.SetActive(false);
            }
        }
    }
}
