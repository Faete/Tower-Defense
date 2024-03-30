using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] CritterLoader critterLoader;
    SaveData savedata = new SaveData();
    public List<Critter> critters;
    public int catchers;

    void Start(){
        Load();
    }

    void Save(){
        savedata.SaveCritters(critters);
        savedata.catchers = catchers;
        string json = JsonUtility.ToJson(savedata);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/critters.json", json);
    }

    void Load(){
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/critters.json");
        savedata = JsonUtility.FromJson<SaveData>(json);
        List<SaveableCritter> saveableCritters = savedata.LoadCrittersAsSavable();
        List<Critter> critterList = new List<Critter>();
        foreach(SaveableCritter saveableCritter in saveableCritters){
            critterList.Add(critterLoader.LoadCritter(saveableCritter));
        }
        critters = critterList;

        catchers = savedata.catchers;
    }
}
