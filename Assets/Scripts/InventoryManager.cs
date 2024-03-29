using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] CritterLoader critterLoader;
    public List<Critter> critters;
    public int catchers;

    void Start(){
        Load();
    }

    void Save(){
        string savefile = "";
        foreach(Critter critter in critters){
            SaveableCritter saveableCritter = new SaveableCritter(critter);
            savefile += JsonUtility.ToJson(saveableCritter);
            savefile += '\n';
        }

        System.IO.File.WriteAllText(Application.persistentDataPath + "/critters.json", savefile);
    }

    void Load(){
        List<Critter> critterList = new List<Critter>();
        string savefile = System.IO.File.ReadAllText(Application.persistentDataPath + "/critters.json");
        string[] savefileSplit = savefile.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
        foreach(string json in savefileSplit){
            SaveableCritter saveableCritter = JsonUtility.FromJson<SaveableCritter>(json);
            critterList.Add(critterLoader.LoadCritter(saveableCritter));
        }
        critters = critterList;
    }
}
