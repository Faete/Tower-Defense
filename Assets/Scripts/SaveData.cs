using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public string critters;
    public int catchers;
    public int level;
    public float volume;

    public void SaveCritters(List<Critter> critterList){
        string critterString = "";
        foreach(Critter critter in critterList){
            SaveableCritter saveableCritter = new SaveableCritter(critter);
            critterString += JsonUtility.ToJson(saveableCritter);
            critterString += '\n';
        }
        critters = critterString;
    }

    public List<SaveableCritter> LoadCrittersAsSavable(){
        List<SaveableCritter> saveableCritters = new List<SaveableCritter>();
        string[] crittersSplit = critters.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
        foreach(string json in crittersSplit){
            SaveableCritter saveableCritter = JsonUtility.FromJson<SaveableCritter>(json);
            saveableCritters.Add(saveableCritter);
        }
        return saveableCritters;
    }
}
