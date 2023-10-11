using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePersist : MonoBehaviour
{
    void OnDisable() => Save();

    void OnEnable() => Load();
    
    public void Load()
    {
        
        foreach (var persist in FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveState>())
        {
            persist.Load();
        }

    }
    public void Save()
    {
       
        foreach (var persist in FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveState>())
        {
            persist.Save();
           
        }
            

    }
}

interface ISaveState
{
    void Save();
    void Load();
}
