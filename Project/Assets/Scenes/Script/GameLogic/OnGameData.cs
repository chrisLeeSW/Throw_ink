using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

using SaveDataVersionCurrent = SaveDataV3;

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

    private bool isTutorialClear;
    private float soundVolum = 80f;
    private float cameraDistance = 5f;
    private float sensitivity = 1f;

    public Color gameColor = Color.black;

    private bool isOnePlaying = false;

    private AudioSource mainAudio;
    public AudioClip gameBgm;
    public AudioMixer audioMixer;
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
    public float SoundVolum
    {
        get { return soundVolum; }
        set { soundVolum = value; }
    }
    public float CameraDistance
    {
        get { return cameraDistance; }
        set { cameraDistance = value; }
    }
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    StageTable stageTable;
    private void Awake()
    {
        mainAudio =GetComponent<AudioSource>();
        //mainAudio.clip = gameBgm;
        //mainAudio.Play();


        DontDestroyOnLoad(gameObject);
        stageTable = new StageTable();
        stageTable.GetStageName(stageNames);
        InitNowAndPrevSceneName();



        StageData data = new StageData();
        data.isClear = false;
        data.resultStar = 0;

        for (int i = 0; i < stageNames.Count; i++)
        {
            datas.Add(stageNames[i], data);
        }

        Load();

        SceneManager.LoadScene(MainSceneName);
    }
    private void FixedUpdate()
    {
       
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
        savedata.isTutorialClear = isTutorialClear;
        savedata.soundVolum =soundVolum;
        savedata.cameraDistance = cameraDistance;
        savedata.sensitivity = sensitivity;
        savedata.redValue = gameColor.r;
        savedata.greenValue = gameColor.g;
        savedata.blueValue = gameColor.b;
        SaveLoadSystem.Save(savedata, "GameData.json");
    }
    public void Load()
    {
      
        var path = Path.Combine(Application.persistentDataPath, "GameData.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            JObject jsonObject = JObject.Parse(json);
            string dataString = jsonObject["data"].ToString();
            var readListData = JsonConvert.DeserializeObject<Dictionary<string, StageData>>(dataString);

            foreach (var reader in readListData)
            {
                StageDataSetting(reader.Key, reader.Value.isClear, reader.Value.resultStar);
                //datas.Add(reader.Key, reader.Value);
                //StageDataSetting(reader.Key, reader.Value.isClear,reader.Value.resultStar);
            }
            isTutorialClear = jsonObject["isTutorialClear"].Value<bool>();
            soundVolum = jsonObject["soundVolum"].Value<float>();
            cameraDistance = jsonObject["cameraDistance"].Value<float>();
            sensitivity = jsonObject["sensitivity"].Value<float>();
            if (!isOnePlaying)
            {
                gameColor.r = jsonObject["redValue"].Value<float>();
                gameColor.g = jsonObject["greenValue"].Value<float>();
                gameColor.b = jsonObject["blueValue"].Value<float>();
            }
        }

        isOnePlaying = true;
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
    public int GetStageResulStar(string name)
    {
        return datas[name].resultStar;
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
    public bool GetStageClear(string name)
    {
       foreach(var getter in datas)
        {
            if(getter.Key ==name)
            {
                return getter.Value.isClear;
            }
        }
       return false;    
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
