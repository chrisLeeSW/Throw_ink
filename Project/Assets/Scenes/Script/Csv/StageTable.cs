using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class StageTable : DataTable
{
    public struct GameData
    {
        public string StageSceneName { get; set; }
        public int ChapterType { get; set; }
        public  int StageNumber { get; set; }
        public int ObjectIsRandomMode { get; set; }
        public float ObjectSpwanTime { get; set; }
        public  float ObjectLifeTime { get; set; }
        public float ObjectInkAreaCondition { get; set; }
        public float ObjectMoveSpeed { get; set; }
        public float ObjectHoldTime { get; set; }
    }
    protected Dictionary<string, GameData> dic = new Dictionary<string, GameData>();
    public StageTable()
    {
        path = "Table/StageTable"; 
        Load();
    }
    public override void Load()
    {
        var csvFileText = Resources.Load<TextAsset>(path);
        TextReader reader = new StringReader(csvFileText.text);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<GameData>();

        foreach (var record in records)
        {
            dic.Add(record.StageSceneName, record);
        }

    }
    public GameData GetGameData(string id)
    {
        if (!dic.ContainsKey(id))
        {
            return default; 
        }
        return dic[id];
    }

    public void GetStageName(List<string> stageNames)
    {
        foreach (var record in dic.Keys)
        {
            stageNames.Add(record);
        }
    }
}
