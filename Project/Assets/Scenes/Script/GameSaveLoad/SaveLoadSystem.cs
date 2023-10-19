using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SaveDataVersionCurrent = SaveDataV3;
public static class SaveLoadSystem
{
    public static int SaveDataVersion { get; private set; } = 1;
    public static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}";
        }
    }
    public static void Save(SaveData data, string fileName)
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }


        var path = Path.Combine(SaveDirectory, fileName);
        using (var writer = new JsonTextWriter(new StreamWriter(path)))
        {
            var serialize = new JsonSerializer();
            serialize.Serialize(writer, data);
        }
    }

    public static SaveData Load(string fileName)
    {
        var path = Path.Combine(SaveDirectory, fileName);
        if (!File.Exists(path))
            return null;

        SaveData data = null;
        int version = 0;

        var json = File.ReadAllText(path);

        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var jobj = JObject.Load(reader);
            version = jobj["Version"].Value<int>(); 
        }
        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var serializer = new JsonSerializer();
            switch (version)
            {
                case 1:
                    data = serializer.Deserialize<SaveDataV1>(reader);
                    break;
                case 2:
                    data = serializer.Deserialize<SaveDataV2>(reader);
                    break;
                case 3:
                    data = serializer.Deserialize<SaveDataV2>(reader);
                    break;
            }

            while (data.Version < SaveDataVersion)
            {
                data = data.VersionUp();
            }
        }
        return data;
    }
}