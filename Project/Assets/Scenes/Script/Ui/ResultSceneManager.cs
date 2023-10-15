using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    public static ResultSceneManager instance
    {
        get
        {
            if (uiRestltManagerSingleTon == null)
            {
                uiRestltManagerSingleTon = FindObjectOfType<ResultSceneManager>();
            }

            return uiRestltManagerSingleTon;
        }
    }
    private static ResultSceneManager uiRestltManagerSingleTon;

    private string sceneName;

    public void LoadMainLobby()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameBuildScene"); // 추후에서 씬을 데이터를 받게 저장해야함
    }
}
