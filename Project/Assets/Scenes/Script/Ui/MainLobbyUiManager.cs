using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobbyUiManager : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("ChpaterSelectScene");
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void OnExitButton()
    {
        Debug.Log("OnExitButton");
        Application.Quit();
    }
}
