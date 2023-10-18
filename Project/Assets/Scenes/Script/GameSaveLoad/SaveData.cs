using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageData
{
    public bool isClear;
    public int resultStar;
    public StageData(bool clear,int reuslt)
    {
        isClear = clear;
        resultStar = reuslt;    
    }
}
public abstract class SaveData
{
    public int Version { get; set; }

    public abstract SaveData VersionUp();
}

public class SaveDataV1 : SaveData
{
    public SaveDataV1()
    {
        Version = 1;
    }
    public int GetVersion()
    {
        return Version;
    }

    public Dictionary<string, StageData> data =new Dictionary<string, StageData>();

    public override SaveData VersionUp()
    {
        var savedata = new SaveDataV2();
        savedata.data = data;
        return null;
    }
}

public class SaveDataV2 : SaveData
{
    public SaveDataV2()
    {
        Version = 2;
    }
    public int GetVersion()
    {
        return Version;
    }

    public Dictionary<string, StageData> data = new Dictionary<string, StageData>();

    public bool isTutorialClear; // test ÇÊ¿ä
    public float soundVolum;
    public float cameraDistance;
    public float sensitivity;

    public override SaveData VersionUp()
    {
        var savedata = new SaveDataV3();
        savedata.data = data;
        savedata.isTutorialClear = isTutorialClear;
        savedata.soundVolum = soundVolum;
        savedata.cameraDistance = cameraDistance;   
        savedata.sensitivity = sensitivity; 
        return null;
    }
}

public class SaveDataV3 : SaveData
{
    public SaveDataV3()
    {
        Version = 3;
    }
    public int GetVersion()
    {
        return Version;
    }

    public Dictionary<string, StageData> data = new Dictionary<string, StageData>();

    public bool isTutorialClear; 
    public float soundVolum;
    public float cameraDistance;
    public float sensitivity;
    public float redValue;
    public float greenValue;
    public float blueValue;

    public override SaveData VersionUp()
    {
        return null;
    }
}

