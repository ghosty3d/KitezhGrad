using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsStorage
{
    public List<Level> levelsList = new List<Level>();

    public LevelsStorage()
    {
        
    }

    public void CreateNewLevel(Level a_NewLevel)
    {
        if (levelsList == null)
        {
            levelsList = new List<Level>();
        }

        if (levelsList.Find(q => q.levelID == a_NewLevel.levelID) != null)
        {
            return;
        }

        levelsList.Add(a_NewLevel);

        Serializer.SerializeObject(this, ConfigManager.EditorLevelsConfigPath);
    }
}
