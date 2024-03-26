using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Builder : MonoBehaviour
{
    public Tilemap tilemap;
    public Critter critter;
    Vector3 offset = new Vector3(0.5f, 0.5f, 0);
    [SerializeField] GameObject towerPrefab;

    public int critterIdx;
    public Inventory inventory;

    void Start(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = critter.sprite;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.75f);
    }
    void Update()
    { 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = tilemap.WorldToCell(mousePos);
        Vector3 tileWorldPos = tilemap.CellToLocal(tilePos);
        transform.position = tileWorldPos + offset;
        if(Input.GetMouseButtonDown(0)){
            if(Physics2D.Raycast(transform.position, Vector3.zero)) Destroy(gameObject);
            else{
                GameObject towerObject = Instantiate(towerPrefab, transform.position, Quaternion.identity);
                Tower towerComponent = towerObject.GetComponent<Tower>();
                towerComponent.range = critter.attackRange;
                towerComponent.reloadTime = critter.reloadTime;
                towerObject.GetComponent<SpriteRenderer>().sprite = critter.sprite;
                towerComponent.projectileSpeed = critter.projectileSpeed;
                towerComponent.projectilePower = critter.attackPower;
                towerComponent.projectileSprite = critter.projectileSprite;

                inventory.critters.Remove(inventory.critters[critterIdx]);
                Destroy(gameObject);
            }
        }
        if(Input.GetMouseButtonDown(1)) Destroy(gameObject);
    }
}
