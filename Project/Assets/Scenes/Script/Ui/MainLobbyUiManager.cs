using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobbyUiManager : MonoBehaviour
{
    public static MainLobbyUiManager instance
    {
        get
        {
            if (uiMainLobbyManagerSingleTon == null)
            {
                uiMainLobbyManagerSingleTon = FindObjectOfType<MainLobbyUiManager>();
            }

            return uiMainLobbyManagerSingleTon;
        }
    }
    private static MainLobbyUiManager uiMainLobbyManagerSingleTon;
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void OnExitButton()
    {
        Debug.Log("OnExitButton");
        Application.Quit();
    }
}
