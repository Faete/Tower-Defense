using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject builderPrefab;
    [SerializeField] Tilemap tilemap;

    public void Build(Critter critter){
        GameObject builderObject = Instantiate(builderPrefab, transform.position, Quaternion.identity);
        Builder builder = builderObject.GetComponent<Builder>();
        builder.tilemap = tilemap;
        builder.critter = critter;
    }
}
