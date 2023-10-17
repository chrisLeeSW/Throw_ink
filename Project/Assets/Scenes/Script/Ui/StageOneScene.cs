using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageOneScene : MonoBehaviour
{
    public List<GameObject> lockStageObjects;
    private int currentLockStageObject ;
    public List<GameObject> unlockStageObjects;
    private int currentUnLockStageObjects=1;


    private void Awake()
    {
        currentLockStageObject = 0; // Save파일 해서 현재 스테이를 저장
        OnGameData.instance.NowSceneName = SceneManager.GetActiveScene().name;
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
        SceneManager.LoadScene(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]); 
    }
    public void LoadChapter1Stage1_2()
    {
        OnGameData.instance.CurrentData = 1;
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
}
