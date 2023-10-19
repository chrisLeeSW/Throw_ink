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


    public bool IsClear
    {
        get; set;
    }
    public bool isPlay
    { get; set; }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    
        result = OnGameData.instance.ResultStagePlay;
        // result는 GameData에서 받아오기
        switch (result)
        {
            case 0:
                IsClear = false;
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
    private void Update()
    {
        if (IsClear && !isPlay)
        {
            isPlay= true;   
            audioSource.PlayOneShot(clearClip);
        }
        else if(!IsClear && !isPlay)
        {
            isPlay = true;
            audioSource.PlayOneShot(failClip);
        }
    }

    private void AddData(bool b)
    {
        IsClear = true;
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
        OnGameData.instance.NowSceneName = OnGameData.instance.stageNames[check];
        SceneManager.LoadScene(OnGameData.instance.stageNames[check]);
    }
}
