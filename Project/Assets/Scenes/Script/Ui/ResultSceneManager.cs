using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    public GameObject clearGameObject;
    public GameObject failGameObject;

    public List<GameObject> satrGameObject;
    public List<GameObject> resultTextGameObject;
    public List<GameObject> rankGameObject;
    [SerializeField, Range(0, 3)]
    private int result;

    private AudioSource audioSource;
    public AudioClip clearClip;
    public AudioClip failClip;  
    private void Awake()
    {
        result = OnGameData.instance.ResultStagePlay;
        // result는 GameData에서 받아오기
        switch (result)
        {
            case 0:
                audioSource.PlayOneShot(failClip);
                failGameObject.SetActive(true);
                AddData(false);
                break;
            case 1:
                ClearTypeResult(result);
                AddData(true);
                break;
            case 2:
                ClearTypeResult(result);
                AddData(true);
                break;
            case 3:
                ClearTypeResult(result);
                AddData(true);
                break;
        }
    }

    private void AddData(bool b)
    {
        audioSource.PlayOneShot(clearClip);
        if (OnGameData.instance.GetStageClear(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]))
        {
            if (OnGameData.instance.GetStageResulStar(OnGameData.instance.stageNames[OnGameData.instance.CurrentData]) < result)
            {
                OnGameData.instance.StageDataSetting(OnGameData.instance.stageNames[OnGameData.instance.CurrentData], b, result);
            }
            else
                return;
        }
        else
            OnGameData.instance.StageDataSetting(OnGameData.instance.stageNames[OnGameData.instance.CurrentData], b, result);
    }
    private void ClearTypeResult(int starCount)
    {
        var type = starCount - 1;
        clearGameObject.SetActive(true);
        for (int i=0; i<=type;++i)
        {
            satrGameObject[i].SetActive(true);
        }
        resultTextGameObject[type].SetActive(true);
        rankGameObject[type].SetActive(true);
    }
    public void LoadMainLobby()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(OnGameData.instance.NowSceneName); 
    }

    public void NextGameButton()
    {
       var check = ++OnGameData.instance.CurrentData;
        if(check>=2 && check <=3)
        {
            check = 4;
        } // stage 1-3 / 1-4가 없어서
        else if(check>=6 && check <=7) 
        {
            check = 4;
        }

        SceneManager.LoadScene(OnGameData.instance.stageNames[check]);
    }
}
