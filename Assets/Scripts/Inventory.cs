using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public List<Critter> critters;
    public int catchers;

    public void AddCritter(Critter critter)
    {
        critters.Add(critter);
    }


    //Save Inventory as JSON
    void Save(){
        string json = JsonUtility.ToJson(critters);
        Debug.Log(json);
    }

}
