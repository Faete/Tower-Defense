using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject builderPrefab;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Inventory inventory;
    [SerializeField] List<GameObject> buttons;

    List<Critter> critterWindow;
    int critterWindowNum = 0;

    void Update(){
        SetCritterWindow();
    }

    void SetCritterWindow(){
        foreach(GameObject button in buttons){
            button.SetActive(false);
        }
        critterWindow = new List<Critter>();
        critterWindow.AddRange(inventory.critters.Skip(critterWindowNum * 4).Take(4));
        for(int i = 0; i < critterWindow.Count(); ++i){
            buttons[i].SetActive(true);
            buttons[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = critterWindow[i].sprite;
        }

    }

    public void Build(int idx){
        Critter critter = critterWindow[idx];
        GameObject builderObject = Instantiate(builderPrefab, transform.position, Quaternion.identity);
        Builder builder = builderObject.GetComponent<Builder>();
        builder.tilemap = tilemap;
        builder.critter = critter;
        builder.critterIdx = critterWindowNum * 4 + idx;
        builder.inventory = inventory;
    }
}
