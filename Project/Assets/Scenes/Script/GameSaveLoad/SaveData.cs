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
        //var data = new SaveDataV2();
        //data.Gold = Gold;
        return null;
    }
}

