using UnityEngine;
using System.IO;
using System.Collections;

public static class ConfigManager
{
    #if UNITY_EDITOR
    public static string EditorLevelsConfigPath = Application.dataPath + "/Config/Levels/levels.json";
    public static string EditorPlayerConfigPath = Application.dataPath + "/Config/Levels/player.json";
    #else
    public static string RuntimeLevelsConfigPath = Application.dataPath + "/Config/Levels/levels.json";
    public static string RuntimePlayerConfigPath = Application.dataPath + "/Config/Levels/player.json";
    #endif
}
