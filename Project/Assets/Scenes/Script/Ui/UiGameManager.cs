using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGameManager : MonoBehaviour
{
    public static UiGameManager instance
    {
        get
        {
            if (uiGameManagerSingleTon == null)
            {
                uiGameManagerSingleTon = FindObjectOfType<UiGameManager>();
            }

            return uiGameManagerSingleTon;
        }
    }
    private static UiGameManager uiGameManagerSingleTon;

    public Slider cameraDistance;
    public Slider sens;
    public GameObject settingBackGround;
    

    [Header("게임 내 UI -> ClearBar")]
    public Image clearBar;
    private float prevClearBarAmount;
    private float clearBarAmount; 
    private float inceraCllearBarDuration;
    private float increasClearBarAmount=0.25f;
    private bool inkAreaCheking;

    [Header("게임 내 UI -> Timer")]
    public Image timerUi;
    public float gameTimeDurtation = 90f;
    public float gameTime = 90f;
    public bool isClear;
    public bool isGameover;
    [Header("플레이어 목숨")]
    public List<GameObject> playerLife;
    private int currentPlayerLife;

    public Button pauseButton;
    public float IncreasBar
    {
        get { return increasClearBarAmount; }
        set { increasClearBarAmount = value;  }
    }
    public bool IsClear
    {
        get { return isClear; }
        set { isClear = value; }
    }
    public bool IsGameover
    {
        get { return isGameover; }
        set { isGameover = value; }
    }
    public bool InkAreaChecking
    {
        get { return inkAreaCheking; }
        set { inkAreaCheking = value; }
    }

    private void Awake()
    {
        currentPlayerLife = playerLife.Count - 1;
        pauseButton.onClick.AddListener(Pause);
    }
    private void Update()
    {
        gameTime-=Time.deltaTime; // 시간을 1에서 빼는중
        timerUi.fillAmount = gameTime / gameTimeDurtation;

        if(gameTime <=0.0f)
        {
            isGameover = true;
            GameManager.instance.IsGameOver = true;
        }

        if (inkAreaCheking)
        {
            prevClearBarAmount += Time.deltaTime * increasClearBarAmount;
            clearBar.fillAmount = prevClearBarAmount;
            if(prevClearBarAmount > inceraCllearBarDuration)
            {
                InkAreaChecking = false;
            }
            if (inceraCllearBarDuration >= 1.0f)
            {
                isClear = true;
                GameManager.instance.IsClear = true;
            }
        }
       
    }

    public void FloatInkAreaCheckingClearBarAmountIncrease()
    {
        inceraCllearBarDuration += increasClearBarAmount;
    }
    public void PlayerLifeDecrease()
    {
        playerLife[currentPlayerLife].SetActive(false);
        currentPlayerLife--;
        if (currentPlayerLife < 0)
            GameManager.instance.IsPlayerDie = true;
    }
    public float GetGameResult()
    {
        return clearBar.fillAmount;
    }
    

    public void Pause()
    {
        Time.timeScale = 0f;
        GamePause(true);
        settingBackGround.SetActive(true);
    }

    public void ExsitPause()
    {
        Time.timeScale = 1f;
        settingBackGround.SetActive(false);
        GamePause(false);
    }

    public void LoadMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(OnGameData.instance.MainSceneName);
        GamePause(false);
    }
    public void ReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(OnGameData.instance.NowSceneName);
        GamePause(false);
    }

    private void GamePause(bool p)
    {
        GameManager.instance.IsPause = p;
    }

    public void CamerDistanceController()
    {
        OnGameData.instance.CameraDistance = cameraDistance.value;
        GameManager.instance.cameraaMove.DistanceFromPlayer = cameraDistance.value;
    }

    public void SenstiveController()
    {
        OnGameData.instance.Sensitivity = sens.value;
        GameManager.instance.XRoationSpeed = sens.value;
    }
}
