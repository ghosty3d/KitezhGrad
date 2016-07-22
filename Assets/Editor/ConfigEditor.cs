using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class ConfigEditor : EditorWindow
{
    public Level newLevel;

    [MenuItem("Kitezhgrad/Config/LevelsConfig")]
    static void Init()
    {
        ConfigEditor window = (ConfigEditor)EditorWindow.GetWindow (typeof (ConfigEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label(string.Format("Levels Config Path:\n{0}", ConfigManager.EditorLevelsConfigPath));
        GUILayout.Label(string.Format("Player Config Path:\n{0}", ConfigManager.EditorPlayerConfigPath));

        if (!File.Exists(ConfigManager.EditorLevelsConfigPath))
        {
            LevelsManager.levelsStorage = new LevelsStorage();

            if (GUILayout.Button("Create Levels Config", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                Serializer.SerializeObject(LevelsManager.levelsStorage, ConfigManager.EditorLevelsConfigPath);
            }  
        }
        else
        {
            if (GUILayout.Button("Load Levels Config", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                LevelsManager.LoadLevelsData();
            }  
        }

        if(LevelsManager.levelsStorage != null)
        {
            GUILayout.Label(string.Format("Levels Count: {0}",  LevelsManager.levelsStorage.levelsList.Count));

            if (GUILayout.Button("Create New Level", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                newLevel = new Level();
            }

            if (newLevel != null)
            {
                newLevel.levelID = EditorGUILayout.IntField("Level ID:", newLevel.levelID);
                newLevel.levelName = EditorGUILayout.TextField("Level Name:", newLevel.levelName);

                if (GUILayout.Button("Update Levels Config", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
                {
                    LevelsManager.levelsStorage.CreateNewLevel(newLevel);
                }  
            }

            if(LevelsManager.levelsStorage.levelsList.Count > 0)
            {
                for(int i = 0; i < LevelsManager.levelsStorage.levelsList.Count; i++)
                {
                    if (GUILayout.Button(string.Format("Play {0}", LevelsManager.levelsStorage.levelsList[i].levelName), GUILayout.ExpandWidth(true), GUILayout.Height(32)))
                    {
                        LevelsManager.LoadLevelByName(LevelsManager.levelsStorage.levelsList[i].levelName);
                    }  
                }
            }
        }
    }
}
