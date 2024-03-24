using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Builder : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject tower;
    Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    void Start(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tower.GetComponent<SpriteRenderer>().sprite;
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
            else Instantiate(tower, transform.position, Quaternion.identity);
        }
    }
}
