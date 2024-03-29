using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject builderPrefab;
    [SerializeField] GameObject catcherPrefab;
    [SerializeField] Tilemap tilemap;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] List<GameObject> critterButtons;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] GameObject catcherButton;

    List<Critter> critterWindow;
    int critterWindowNum = 0;

    void Update(){
        SetCritterWindow();
        DisplayArrowButtons();
        CatcherButtonText();
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

    void CatcherButtonText(){
        catcherButton.GetComponentInChildren<TextMeshProUGUI>().text = inventoryManager.catchers.ToString();
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

    public void Catcher(){
        if(inventoryManager.catchers > 0){
            GameObject catcherObject = Instantiate(catcherPrefab, transform.position, Quaternion.identity);
            Catcher catcher = catcherObject.GetComponent<Catcher>();
            catcher.tilemap = tilemap;
            catcher.inventoryManager = inventoryManager;
        }
    }
    public void IncCritterWindow(){
        critterWindowNum++;
    }

    public void DecCritterWindow(){
        critterWindowNum--;
    }
}
