using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Critter> critters;

    void Start(){
        Save();
    }

    public void AddCritter(Critter critter)
    {
        critters.Add(critter);
    }

    void Save(){
    }
}
