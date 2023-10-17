using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChpaterSelectUiManager : MonoBehaviour
{

    private void Awake()
    {
        OnGameData.instance.NowSceneName = SceneManager.GetActiveScene().name;
        if(OnGameData.instance.PrevSceneName == OnGameData.instance.NowSceneName )
        {
            OnGameData.instance.PrevSceneName = OnGameData.instance.MainSceneName;
        }
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

    public void LoadStage1SelectScene()
    {
        OnGameData.instance.PrevSceneName = OnGameData.instance.NowSceneName;
        SceneManager.LoadScene("STAGE1_V1.0");
    }

    public void BackButton()
    {
       SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }

}
