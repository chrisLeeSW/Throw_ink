using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChpaterSelectUiManager : MonoBehaviour
{
    public List<string> chpatersName = new List<string>();
    public List<int> chapterStarDuration =new List<int>();

    public List<GameObject> openWinodw= new List<GameObject>();
    private int CurrentOpenWinodw = 1;
    public List<GameObject> lockWindow = new List<GameObject>();

    public List<TextMeshProUGUI> textMeshPros = new List<TextMeshProUGUI>();
    private List<int> textMeshClearNumber = new List<int>();     // 결과값 읽어올거임

    public List<Image> nowClearBar= new List<Image>();  

    private void Awake()
    {
        for(int i = 0;i<openWinodw.Count;++i)
        {
            if (i == 0)
            {
                lockWindow[i].SetActive(false);
                openWinodw[i].SetActive(true);
            }
            else
            {
                lockWindow[i].SetActive(true);
                openWinodw[i].SetActive(false);
            }
        }     
        
        OnGameData.instance.NowSceneName = SceneManager.GetActiveScene().name;
        if(OnGameData.instance.PrevSceneName == OnGameData.instance.NowSceneName )
        {
            OnGameData.instance.PrevSceneName = OnGameData.instance.MainSceneName;
        }

        SettingChapterOpen();
        SettingClearStar();
        SettingClearText();
        SettingClearBar();
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene(OnGameData.instance.MainSceneName);
    }

    public void LoadSettingScene()
    {
        OnGameData.instance.PrevSceneName = OnGameData.instance.NowSceneName;
        SceneManager.LoadScene(OnGameData.instance.SettingSceneName);
    }

    public void LoadStage1SelectScene()
    {
        OnGameData.instance.PrevSceneName = OnGameData.instance.NowSceneName;
        SceneManager.LoadScene("STAGE1_V1.0");
    }
    public void LoadStage2SelectScene()
    {
        //OnGameData.instance.PrevSceneName = OnGameData.instance.NowSceneName;
        //SceneManager.LoadScene("STAGE2_V1.0");
    }
    public void BackButton()
    {
       SceneManager.LoadScene(OnGameData.instance.PrevSceneName);
    }

    private void SettingChapterOpen()
    {
        for (int i = 0; i < chpatersName.Count; ++i)
        {
            if (OnGameData.instance.GetStageResultClear(chpatersName[i]))
            {
                openWinodw[CurrentOpenWinodw].SetActive(true);
                lockWindow[CurrentOpenWinodw].SetActive(false);

                

                CurrentOpenWinodw++;
            }
        }
    }
    private void SettingClearStar()
    {
        for (int i = 0; i < chpatersName.Count; ++i)
        {
            var result = OnGameData.instance.GetStageResultAllStar(chpatersName[i]);
            textMeshClearNumber.Add(result);
        }
    }
    private void SettingClearText()
    {
        if (textMeshClearNumber.Count != chpatersName.Count)
            return;

        for(int i=0;i< textMeshClearNumber.Count; ++i)
        {
            textMeshPros[i].text = $"{textMeshClearNumber[i]}/{chapterStarDuration[i]}";
        }
    }

    private void SettingClearBar()
    {
        for(int i=0; i< nowClearBar.Count;++i)
        {
            if (i >= textMeshClearNumber.Count || i >= chapterStarDuration.Count)
                nowClearBar[i].fillAmount = 0;
            else
            {
                float result = (float)textMeshClearNumber[i] / chapterStarDuration[i];
                nowClearBar[i].fillAmount = result;
            }
        }
    }

}
