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
    [Header("���� �� UI -> ClearBar")]
    public Image clearBar;
    private float prevClearBarAmount;
    private float clearBarAmount; //private �ٲ����
    private float inceraCllearBarDuration;
    private float increasClearBarAmount;
    private bool inkAreaCheking;

    [Header("���� �� UI -> Timer")]
    public Image timerUi;
    public float gameTimeDurtation = 90f;
    public float gameTime = 90f;
    public bool isClear;
    public bool isGameover;

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

    }
    private void Update()
    {
        gameTime-=Time.deltaTime; // �ð��� 1���� ������
        timerUi.fillAmount = gameTime / gameTimeDurtation;

        if(gameTime <=0.0f)
        {
            isGameover = true;
        }

        if (inkAreaCheking)
        {
            prevClearBarAmount += Time.deltaTime * increasClearBarAmount;
            clearBar.fillAmount = prevClearBarAmount;
            if(prevClearBarAmount > inceraCllearBarDuration)
            {
                InkAreaChecking = false;
            }
        }
        if(inceraCllearBarDuration >=1.0f)
        {
            isClear = true;
        }
    }

    public void FloatInkAreaCheckingClearBarAmountIncrease(float IncreaseBarAmount)
    {
        inceraCllearBarDuration += IncreaseBarAmount;
    }

}
