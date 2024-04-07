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
    AudioSource clickAudioSource;
    string saveFilePath;

    void Start(){
        clickAudioSource = GetComponent<AudioSource>();
        saveFilePath = Application.persistentDataPath + "/critters.json";
    }

    public void NewGame(){
        if(!PlayerPrefs.HasKey("Volume")) PlayerPrefs.SetFloat("Volume", 1f);
        PlayerPrefs.Save();
        clickAudioSource.Play();
        if(!File.Exists(saveFilePath)) NewGameConfirmed();
        else newGameConfirmationCanvas.SetActive(true);
    }

    public void NewGameDenied(){
        clickAudioSource.Play();
        newGameConfirmationCanvas.SetActive(false);
    }

    public void NewGameConfirmed(){
        clickAudioSource.Play();
        savedata = new SaveData();
        savedata.critters = JsonUtility.ToJson(new SaveableCritter(gimbo));
        savedata.catchers = 5;
        savedata.level = 0;
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(saveFilePath, json);
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadGame(){
        clickAudioSource.Play();
        if(System.IO.File.Exists(saveFilePath)) SceneManager.LoadScene("Menu");
    }

    public void Quit(){
        clickAudioSource.Play();
        Application.Quit();
    }

}
