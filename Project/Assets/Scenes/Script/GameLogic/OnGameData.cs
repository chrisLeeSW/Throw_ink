using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

using SaveDataVersionCurrent = SaveDataV1;

public class OnGameData : MonoBehaviour
{
    
    public static OnGameData instance
    {
        get
        {
            if (onGameDataSingleTon == null)
            {
                onGameDataSingleTon = FindObjectOfType<OnGameData>();
            }

            return onGameDataSingleTon;
        }
    }
    private static OnGameData onGameDataSingleTon;



    private Color playerColor;
    public List<string> stageNames;
    private int currentData;

    private string nowSceneName;
    private string prevSceneName;
    private string mainSceneName = "MainScene";
    private string settingSceneName = "SETTINGS-V1.0";

    public Dictionary<string, StageData> datas = new Dictionary<string, StageData>();
    public int CurrentData
    {
        get { return currentData; }
        set { currentData = value; }
    }
    public string NowSceneName
    {
        get { return nowSceneName; }
        set { nowSceneName = value; }
    }
    public string PrevSceneName
    {
        get { return prevSceneName; }
        set { prevSceneName = value; }
    }
    public string MainSceneName
    {
        get { return mainSceneName; }
    }
    public string SettingSceneName
    {
        get { return settingSceneName; }
    }
    public int ResultStagePlay
    {
        get; set;
    }
    StageTable stageTable;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        stageTable = new StageTable();
        stageTable.GetStageName(stageNames);
        InitNowAndPrevSceneName();


        Load();
        //StageData data = new StageData();
        //data.isClear = false;
        //data.resultStar = 0;

        //for(int i=0;i<stageNames.Count;i++)
        //{
        //    datas.Add(stageNames[i], data);
        //}
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Save();
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            foreach (var data in datas)
                Debug.Log($"{data.Key}/{data.Value.isClear}/{data.Value.resultStar}");
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            GetStageResultAllStar("chapter 1");
        }
    }

    private void InitNowAndPrevSceneName()
    {
        PrevSceneName = SceneManager.GetActiveScene().name;
        NowSceneName = SceneManager.GetActiveScene().name;
    }
    public string GetStageName(int index)
    {
        var name = stageTable.GetGameData(stageNames[index]).StageSceneName;

        return name;
    }
    public float GetStageObjectLife(int index)
    {
        var lifeTime = stageTable.GetGameData(stageNames[index]).ObjectLifeTime;

        return lifeTime;
    }

    public float GetStageObjectAreaCondition(int index)
    {
        var condition = stageTable.GetGameData(stageNames[index]).ObjectInkAreaCondition;

        return condition;
    }

    public float GetStageOBjectSpwanTime(int index)
    {
        var spwanTime = stageTable.GetGameData(stageNames[index]).ObjectSpwanTime;

        return spwanTime;
    }

    public float GetStageObjectMoveSpeed(int index)
    {
        var movespeed = stageTable.GetGameData(stageNames[index]).ObjectMoveSpeed;

        return movespeed;

    }

    public float GetStageObjectHoldTime(int index)
    {
        var holdTime = stageTable.GetGameData(stageNames[index]).ObjectHoldTime;

        return holdTime;
    }

 

    public void Save()
    {
        var savedata = new SaveDataVersionCurrent();
        savedata.data = datas;
        SaveLoadSystem.Save(savedata, "GameData.json");

        Debug.Log("SaveComplete");
    }
    public void Load()
    {
        var path = Path.Combine(Application.persistentDataPath, "GameData.json");
        var json = File.ReadAllText(path);

        JObject jsonObject = JObject.Parse(json);
        string dataString = jsonObject["data"].ToString();
        var readListData = JsonConvert.DeserializeObject<Dictionary<string,StageData>>(dataString);

        foreach(var reader in readListData)
        {
            datas.Add(reader.Key, reader.Value);
            //StageDataSetting(reader.Key, reader.Value.isClear,reader.Value.resultStar);
        }
    }

    public int GetStageResultAllStar(string ChapterName)
    {
        int result = 0;
        foreach (var getter in datas)
        {
            if (getter.Key.Contains(ChapterName))
            {
                result += getter.Value.resultStar;
            }
        }
        return result;
    }

    public int GetStageResulStar(int index)
    {
        return datas[stageNames[index]].resultStar;
    }

    public bool GetStageResultClear(string ChapterName)
    {
        foreach(var getter in datas)
        {
            if(getter.Key.Contains(ChapterName) && !getter.Value.isClear)
            {
                return false;
            }
        }

        return true;
    }

    public int GetStageNameByCount(string ChapterName)
    {
        int result = 0;
        foreach (var getter in datas)
        {
            if (getter.Key.Contains(ChapterName))
            {
                result++;
            }
        }
        return result;

    }
    public int GetStageNameByStartIndex(string ChapterName)
    {
        int result = 0;
        foreach (var getter in datas)
        {
            if (getter.Key.Contains(ChapterName))
            {
                return result;
            }
            else result++;
        }
        return -1;
    }

    public bool GetStageClear(int index)
    {
        return datas[stageNames[index]].isClear;
    }
    public void StageDataSetting(string sceneName, bool clear, int result)
    {
        if (datas.ContainsKey(sceneName))
        {
            StageData currentData = datas[sceneName];
            currentData.isClear = clear;
            currentData.resultStar = result;
            datas[sceneName] = currentData;
        }
    }

}
