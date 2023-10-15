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



    private int currentData; // ����Ʈ�� �۾��� �ؼ� ������ �޾ƿð���....Ŭ���� ���Ӱ� ���� �ϴ� ��᳻������ ������ ���ؼ� �����
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
