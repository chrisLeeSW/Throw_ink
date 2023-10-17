using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUiManager : MonoBehaviour
{

    private void Awake()
    {
        OnGameData.instance.NowSceneName = SceneManager.GetActiveScene().name;
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene(OnGameData.instance.MainSceneName);
    }
    public void BackButton()
    {
        SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }
}
