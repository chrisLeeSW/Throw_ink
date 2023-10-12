using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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


    [Header("게임 내 UI -> ClearBar")]
    public Image clearBar;
    private float prevClearBarAmount;
    private float clearBarAmount; //private 바꿔야함
    private float increasClearBarAmount =0.5f;
    private bool inkAreaCheking;

    [Header("게임 내 UI -> Timer")]
    public Image timerUi;
    public float gameTimeDurtation = 120f;
    public float gameTime = 120f; 

    public bool InkAreaChecking
    {
        set { inkAreaCheking = value; }
    }
    

    private void Update()
    {
        gameTime-=Time.deltaTime; // 시간을 1에서 빼는중
        timerUi.fillAmount = gameTime / gameTimeDurtation;


        if (inkAreaCheking)
        {
            prevClearBarAmount += Time.deltaTime * increasClearBarAmount;
            clearBar.fillAmount = prevClearBarAmount;
            if(prevClearBarAmount >increasClearBarAmount)
            {
                InkAreaChecking = false;
            }
        }
        //clearBar.fillAmount = clearBarAmount;
    }

    public void SetFloatInkAreaCheckingClearBarAmount(float clearBarAmount)
    {
        this.clearBarAmount += clearBarAmount;
        if(this.clearBarAmount >=0.95f)
        {
            clearBarAmount = 1.0f;
        }
    }
}
