using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] GameObject firstTimeCompletionText;
    [SerializeField] GameObject critterPanel;
    [SerializeField] GameObject pausePanel;

    bool isPaused;

    List<Critter> critterWindow;
    int critterWindowNum = 0;
    int levelId;
    int firstTimeCompletionBonus;

    void Start(){
        WaveManager wm = Object.FindAnyObjectByType<WaveManager>();
        levelId = wm.levelId;
        firstTimeCompletionBonus = wm.catchersPrize;
    }
    void Update(){
        SetCritterWindow();
        DisplayArrowButtons();
        CatcherButtonText();
        FirstTimeBonusText();
        Pause();
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
            critterButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = $"Lvl: {critterWindow[i].level}";
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

    void FirstTimeBonusText(){
        firstTimeCompletionText.GetComponent<TextMeshProUGUI>().text = $"First Time Competion Bonus: +{firstTimeCompletionBonus} Catchers";
        if(inventoryManager.savedata.level > levelId) firstTimeCompletionText.SetActive(false);
        else firstTimeCompletionText.SetActive(true);
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

    void Pause(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Time.timeScale = 1f;
                isPaused = false;
                pausePanel.SetActive(false);
                critterPanel.SetActive(true);
            } else{
                Time.timeScale = 0f;
                isPaused = true;
                pausePanel.SetActive(true);
                critterPanel.SetActive(false);
            }
        }
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
    }

    public void TitleScreen(){
        SceneManager.LoadScene("StartScreen");
    }

    public void Volume(){

    }

    public void PauseContinue(){
        pausePanel.SetActive(false);
        critterPanel.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f;
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

    public void Continue(){
        SceneManager.LoadScene("Menu");
    }
}
