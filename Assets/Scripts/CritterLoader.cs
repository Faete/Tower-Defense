using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CritterLoader : ScriptableObject
{
    [SerializeField] List<Critter> critterList;
    [SerializeField] List<string>  critterNames;

    public Critter LoadCritter(SaveableCritter saveableCritter){
        if(critterNames.Contains(saveableCritter.name)){
            int location = critterNames.IndexOf(saveableCritter.name);
            Critter critter = Instantiate(critterList[location]);
            critter.level = saveableCritter.level;
            critter.experience = saveableCritter.experience;
            return critter;
        }
        else return null;
    }
}
