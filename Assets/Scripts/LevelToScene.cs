using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelToScene : ScriptableObject
{
    public List<string> sceneNames;

    public string Convert(int level){
        if(level < sceneNames.Count) return sceneNames[level];
        else return "Menu";
    }
}
