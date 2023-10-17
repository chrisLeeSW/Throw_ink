using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        stageTable =new StageTable();
        stageTable.GetStageName(stageNames);
        InitNowAndPrevSceneName();
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
    
}
