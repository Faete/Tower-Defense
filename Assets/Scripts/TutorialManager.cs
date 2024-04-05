using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] List<GameObject> images;
    [SerializeField] List<string> texts;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;
    [SerializeField] GameObject startButton;
    [SerializeField] TextMeshProUGUI tutorialText;

    int idx;

    void Start(){
        idx = 0;
        tutorialText.text = texts[0];
        images[0].SetActive(true);
    }

    void Update(){
        if(idx == texts.Count - 1){
            nextButton.SetActive(false);
            startButton.SetActive(true);
        } else{
            nextButton.SetActive(true);
            startButton.SetActive(false);
        }
        if(idx == 0) prevButton.SetActive(false);
        else prevButton.SetActive(true);
    }

    public void Next(){
        if(idx != texts.Count - 1){
            images[idx].SetActive(false);
            idx++;
            images[idx].SetActive(true);
            tutorialText.text = texts[idx];
        }
    }

    public void Prev(){
        if(idx != 0){
            images[idx].SetActive(false);
            idx--;
            images[idx].SetActive(true);
            tutorialText.text = texts[idx];
        }
    }

    public void Go(){
        SceneManager.LoadScene("Menu");
    }
}
