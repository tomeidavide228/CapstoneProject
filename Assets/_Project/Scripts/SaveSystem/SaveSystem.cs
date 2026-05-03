using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string _path = Application.persistentDataPath + "/save.json";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);
    }

    public static SaveData Load()
    {
        if (!File.Exists(_path))
            return null;

        string json = File.ReadAllText(_path);
        return JsonUtility.FromJson<SaveData>(json);
    }
}

