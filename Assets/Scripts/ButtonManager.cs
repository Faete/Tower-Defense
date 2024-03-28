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
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] List<GameObject> critterButtons;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;

    List<Critter> critterWindow;
    int critterWindowNum = 0;

    void Update(){
        SetCritterWindow();
        DisplayArrowButtons();
    }

    void SetCritterWindow(){
        foreach(GameObject button in critterButtons){
            button.SetActive(false);
        }
        critterWindow = new List<Critter>();
        critterWindow.AddRange(inventoryManager.critters.Skip(critterWindowNum * 4).Take(4));
        for(int i = 0; i < critterWindow.Count; ++i){
            critterButtons[i].SetActive(true);
            critterButtons[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = critterWindow[i].sprite;
        }

    }

    void DisplayArrowButtons(){
        if((critterWindowNum + 1) * 4 > inventoryManager.critters.Count - 1) rightButton.SetActive(false);
        else rightButton.SetActive(true);
        if(critterWindowNum == 0) leftButton.SetActive(false);
        else leftButton.SetActive(true);
    }

    public void Build(int idx){
        Critter critter = critterWindow[idx];
        GameObject builderObject = Instantiate(builderPrefab, transform.position, Quaternion.identity);
        Builder builder = builderObject.GetComponent<Builder>();
        builder.tilemap = tilemap;
        builder.critter = critter;
        builder.critterIdx = critterWindowNum * 4 + idx;
        builder.inventoryManager = inventoryManager;
    }

    public void IncCritterWindow(){
        critterWindowNum++;
    }

    public void DecCritterWindow(){
        critterWindowNum--;
    }
}
