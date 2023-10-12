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
    private float increasClearBarAmount =0.5f;
    private bool inkAreaCheking;

    [Header("���� �� UI -> Timer")]
    public Image timerUi;
    public float gameTimeDurtation = 120f;
    public float gameTime = 120f; 

    public bool InkAreaChecking
    {
        set { inkAreaCheking = value; }
    }
    

    private void Update()
    {
        gameTime-=Time.deltaTime; // �ð��� 1���� ������
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
