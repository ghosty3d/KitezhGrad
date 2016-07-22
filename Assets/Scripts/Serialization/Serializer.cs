using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Object = System.Object;

public static class Serializer
{
    /// <summary>
    /// Serialize the specified a_object and a_Path.
    /// </summary>
    /// <param name="a_object">A object.</param>
    /// <param name="a_Path">A path.</param>
    public static void SerializeObject(object a_object, string a_Path)
    {
        if (!File.Exists(a_Path))
        {
            using(StreamWriter streamWriter = File.CreateText(a_Path))
            {
                
            }
        }

        using (StreamWriter streamWriter = new StreamWriter(a_Path))
        {
            using (JsonTextWriter writer = new JsonTextWriter(streamWriter))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, a_object);
            }
        }

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public static T DeserializeObject<T>(string a_Path)
    {
        if (File.Exists(a_Path))
        {
            using (StreamReader reader = new StreamReader(a_Path))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jsonReader);
                }
            }
        }
        else
        {
            return default(T);
        }
    }
}
