using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableCritter
{
    public string name;
    public int level;
    public int experience;

    public SaveableCritter(Critter critter){
        name = critter.name;
        level = critter.level;
        experience = critter.experience;
    }
}
