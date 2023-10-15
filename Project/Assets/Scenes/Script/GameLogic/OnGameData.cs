using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameData : MonoBehaviour
{
    public static OnGameData instance
    {
        get
        {
            if (onGameDataSingleTon == null)
            {
                onGameDataSingleTon = FindObjectOfType<OnGameData>();
            }

            return onGameDataSingleTon;
        }
    }
    private static OnGameData onGameDataSingleTon;

    private struct GameData
    {
        string stageName;
        int chapterType;
    }



    private int currentData; // 리스트로 작업을 해서 데이터 받아올거임....클래스 새롭게 만들어서 일단 어뜬내용일지 정하지 못해서 대기중
    private string prevSceneName;

    public int CurrentData
    {
        get { return currentData; }
        set { currentData = value; }    
    }
    public string PrevSceneName
    {
        get { return prevSceneName; }   
        set { prevSceneName = value; }  
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

 

}
