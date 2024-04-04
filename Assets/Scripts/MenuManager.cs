using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] LevelToScene levelToScene;
    [SerializeField] GameObject leftArrowButton;
    [SerializeField] GameObject rightArrowButton;
    [SerializeField] TextMeshProUGUI levelName;

    SaveData saveData;
    int displayedLevel;

    void Start()
    {
        saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/critters.json"));
        displayedLevel = saveData.level;
    }

    void Update()
    {
        Arrows();
        LevelText();
    }

    void Arrows(){
        if(displayedLevel == 0) leftArrowButton.SetActive(false);
        else leftArrowButton.SetActive(true);
        if(displayedLevel == saveData.level) rightArrowButton.SetActive(false);
        else rightArrowButton.SetActive(true);
    }

    void LevelText(){
        levelName.text = levelToScene.Convert(displayedLevel);
    }

    public void DisplayNextLevel(){
        displayedLevel++;
    }

    public void DisplayPreviousLevel(){
        displayedLevel--;
    }

    public void StartLevel(){
        SceneManager.LoadScene(levelToScene.Convert(displayedLevel));
    }
}
