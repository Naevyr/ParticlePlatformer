
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Level", menuName = "Campaign", order = 1)]
public class Campaign : ScriptableObject
{
    List<string>  _sceneNames = new List<string>();
    public Scene GetLevel(int index)
    {
        
        return SceneManager.GetSceneByName(_sceneNames[index]); 
        
    }
    
  
}
