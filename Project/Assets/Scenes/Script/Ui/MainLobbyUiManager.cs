using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobbyUiManager : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("CHAPTER_V1.0");
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene(OnGameData.instance.SettingSceneName);
    }

    public void OnExitButton()
    {
        Debug.Log("OnExitButton");
        Application.Quit();
    }
}
