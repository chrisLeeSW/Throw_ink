using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageOneScene : MonoBehaviour
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
    }
    public void LoadBakcButton()
    {
        SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }
    public void LoadChapter1Stage1_1()
    {
        OnGameData.instance.CurrentData = 0;
        OnGameData.instance.PrevSceneName = SceneManager.GetActiveScene().name;
        OnGameData.instance.NowSceneName = OnGameData.instance.GetStageName(OnGameData.instance.CurrentData);
        //SceneManager.LoadScene(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]);
       
       //SceneManager.LoadScene("chapter 2-1");
    }
    public void LoadChapter1Stage1_2()
    {
        OnGameData.instance.CurrentData = 1;
        OnGameData.instance.PrevSceneName = SceneManager.GetActiveScene().name;
        OnGameData.instance.NowSceneName = OnGameData.instance.GetStageName(OnGameData.instance.CurrentData);
        SceneManager.LoadScene(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]);
        //SceneManager.LoadScene("chapter 1-1");
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
                if (num >= stageClearCount)
                    break;

                var temp = OnGameData.instance.GetStageResulStar(num);
                SettingRank(num, temp);
                SettingStart(num, temp);

                lockWindow[num].SetActive(false);
                openWinodw[num].SetActive(true);


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
