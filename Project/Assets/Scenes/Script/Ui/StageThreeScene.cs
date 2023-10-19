using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageThreeScene : MonoBehaviour
{
    public string thisChapterName;
    public List<GameObject> openWinodw;
    public List<GameObject> lockWindow;

    public List<GameObject> stars;
    public List<GameObject> rankObjectes;
    private int stageIndex;

    private int stageClearStartIdnex;
    private int stageClearCount;

    private void Awake()
    {
        Init();
        OnGameData.instance.NowSceneName = SceneManager.GetActiveScene().name;
        ReadClearStage();

        if (OnGameData.instance.NowSceneName == OnGameData.instance.PrevSceneName)
        {
            OnGameData.instance.PrevSceneName = "CHAPTER_V1.0";
        }
    }
    public void LoadBakcButton()
    {
        SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }
    public void LoadChapterStage3_1()
    {
        OnGameData.instance.CurrentData =6;
        OnGameData.instance.PrevSceneName = SceneManager.GetActiveScene().name;
        OnGameData.instance.NowSceneName = OnGameData.instance.GetStageName(OnGameData.instance.CurrentData);
        SceneManager.LoadScene(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]);
    }
    public void LoadChapter1Stage3_2()
    {
        OnGameData.instance.CurrentData = 7;
        OnGameData.instance.PrevSceneName = SceneManager.GetActiveScene().name;
        OnGameData.instance.NowSceneName = OnGameData.instance.GetStageName(OnGameData.instance.CurrentData);
        SceneManager.LoadScene(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]);
    }
    public void LoadChapter1Stage3_3()
    {
        OnGameData.instance.CurrentData = 8;
        OnGameData.instance.PrevSceneName = SceneManager.GetActiveScene().name;
        OnGameData.instance.NowSceneName = OnGameData.instance.GetStageName(OnGameData.instance.CurrentData);
        SceneManager.LoadScene(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(OnGameData.instance.MainSceneName);
    }

    public void LoadSettingScene()
    {
        OnGameData.instance.PrevSceneName = OnGameData.instance.NowSceneName;
        SceneManager.LoadScene(OnGameData.instance.SettingSceneName);
    }

    private void Init()
    {
        stageClearStartIdnex = OnGameData.instance.GetStageNameByStartIndex(thisChapterName);
        stageClearCount = OnGameData.instance.GetStageNameByCount(thisChapterName);

        for (int i=0;i<stars.Count;  ++i)
        {
            stars[i].SetActive(false);
        }
        for(int i=0;i< rankObjectes.Count;++i)
        {
            rankObjectes[i].SetActive(false);
        }


        for (int i = 0; i < openWinodw.Count; i++)
        {
            if (i == 0)
            {
                var temp = OnGameData.instance.GetStageResulStar(stageClearStartIdnex);
                SettingRank(i, temp);
                SettingStart(i, temp);

                lockWindow[i].SetActive(false);
                openWinodw[i].SetActive(true);

            }
            else
            {
                lockWindow[i].SetActive(true);
                openWinodw[i].SetActive(false);
            }
        }
    }

    private void ReadClearStage()
    {
        for(int i= stageClearStartIdnex;i< stageClearStartIdnex+stageClearCount;i++)
        {
            if(OnGameData.instance.GetStageClear(i))
            {
                var num = i + 1;
                if (num >= stageClearStartIdnex + stageClearCount)
                    break;

               
                var temp = OnGameData.instance.GetStageResulStar(num);
                var stage2Num = num %7 +1;
                SettingRank(stage2Num, temp);
                SettingStart(stage2Num, temp);

                lockWindow[stage2Num].SetActive(false);
                openWinodw[stage2Num].SetActive(true);
    
            }
        }
    }

    private void SettingRank(int openWindowIndex,int result)
    {
        int start= openWindowIndex *4;
        rankObjectes[start + result].SetActive(true);
    }

    private void SettingStart(int openWindowIndex, int result)
    {
        int start= openWindowIndex *3;

        for(int i=start;i<start+result;i++)
        {
            stars[i].SetActive(true);   
        }
    }
}
