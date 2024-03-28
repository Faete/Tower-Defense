using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Catcher : MonoBehaviour
{
    public Tilemap tilemap;
    Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    public InventoryManager inventoryManager;

    void Start(){
        Time.timeScale = 0f;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = tilemap.WorldToCell(mousePos);
        Vector3 tileWorldPos = tilemap.CellToLocal(tilePos);
        transform.position = tileWorldPos + offset;
        if(Input.GetMouseButtonDown(0)){
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector3.zero);
            foreach(RaycastHit2D hit in hits){
                if(hit.transform.gameObject.CompareTag("Enemy")){
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    if(enemy.canCatch){
                        inventoryManager.critters.Add(enemy.critter);
                        Destroy(hit.transform.gameObject);
                        break;
                    }
                }
            }
            Destroy(gameObject);
            Time.timeScale = 1f;
        }
        if(Input.GetMouseButtonDown(1)){
            Destroy(gameObject);
            Time.timeScale = 1f;
        }
    }
}
