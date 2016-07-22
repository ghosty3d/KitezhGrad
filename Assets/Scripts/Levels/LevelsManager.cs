using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public static class LevelsManager
{
    public static LevelsStorage levelsStorage;

    public static void LoadLevelsData()
    {
        levelsStorage = Serializer.DeserializeObject<LevelsStorage>(ConfigManager.EditorLevelsConfigPath);
    }

    public static void LoadLevelByID(int id)
    {
        if (levelsStorage.levelsList.Find(q => q.levelID == id) != null)
        {
            SceneManager.LoadScene(id);
        }
    }

    public static void LoadLevelByName(string a_LevelName)
    {
        if (levelsStorage.levelsList.Find(q => q.levelName == a_LevelName) != null)
        {
            #if UNITY_EDITOR
            string levelScenePath = Application.dataPath + "/Scenes/" + a_LevelName + ".unity";
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(levelScenePath);
            #else
            SceneManager.LoadScene(a_LevelName);
            #endif
        }
    }
}
