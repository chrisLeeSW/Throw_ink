using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageOneScene : MonoBehaviour
{
    private string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    public void LoadBakcButton()
    {
        SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }
    public void LoadChapter1Stage1_1()
    {
        OnGameData.instance.CurrentData = 0;
        SceneManager.LoadScene("Stage1_1Scene"); 
    }
    public void LoadChapter1Stage1_2()
    {
        OnGameData.instance.CurrentData = 0;
        SceneManager.LoadScene("Stage1_2Scene");
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("SettingScene");
    }
}
