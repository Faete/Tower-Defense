using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{

    [SerializeField] Critter gimbo;
    [SerializeField] GameObject newGameConfirmationCanvas;
    SaveData savedata;
    string saveFilePath;

    void Start(){
        saveFilePath = Application.persistentDataPath + "/critters.json";
    }

    public void NewGame(){
        if(!File.Exists(saveFilePath)) NewGameConfirmed();
        else newGameConfirmationCanvas.SetActive(true);
    }

    public void NewGameDenied(){
        newGameConfirmationCanvas.SetActive(false);
    }

    public void NewGameConfirmed(){
        savedata = new SaveData();
        savedata.critters = JsonUtility.ToJson(new SaveableCritter(gimbo));
        savedata.catchers = 5;
        savedata.level = 0;
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(saveFilePath, json);
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadGame(){
        if(System.IO.File.Exists(saveFilePath)) SceneManager.LoadScene("Menu");
    }

    public void Quit(){
        Application.Quit();
    }

}
