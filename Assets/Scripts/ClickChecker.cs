using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickChecker : MonoBehaviour
{
    Tower selectedTower;
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0)){
            if(!EventSystem.current.IsPointerOverGameObject()){
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);
                if(hit && hit.transform.CompareTag("Tower")){
                    if(selectedTower != null) Deselect();
                    selectedTower = hit.transform.GetComponent<Tower>();
                    selectedTower.transform.GetChild(0).gameObject.SetActive(true);
                }else Deselect();
            } 
        }
    }

    public void Deselect(){
        if(selectedTower != null){
            selectedTower.transform.GetChild(0).gameObject.SetActive(false);
            selectedTower = null;
        }
    }
}
