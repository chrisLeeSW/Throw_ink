using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChpaterSelectUiManager : MonoBehaviour
{
    private string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadSettingScene()
    {
        OnGameData.instance.PrevSceneName =sceneName;
        SceneManager.LoadScene("SettingScene");
    }

    public void LoadStage1SelectScene()
    {
        OnGameData.instance.PrevSceneName = sceneName;
        SceneManager.LoadScene("Stage1SelectScene");
    }

}
