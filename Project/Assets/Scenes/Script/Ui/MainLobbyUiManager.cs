using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobbyUiManager : MonoBehaviour
{
    private void Awake()
    {
        OnGameData.instance.NowSceneName = SceneManager.GetActiveScene().name;
        OnGameData.instance.PrevSceneName= SceneManager.GetActiveScene().name;
    }
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("CHAPTER_V1.0");
    }

    public void LoadCustomerScene()
    {
        
        SceneManager.LoadScene("Customizing");
    }
    public void LoadSettingScene()
    {
        SceneManager.LoadScene(OnGameData.instance.SettingSceneName);
    }

    public void OnExitButton()
    {
        OnGameData.instance.Save();
        Application.Quit();
    }
}
