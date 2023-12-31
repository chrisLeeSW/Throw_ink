using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSubController : MonoBehaviour
{
    private InkAreaChecker inkAreaChecker;

    public Image cubeImage;
    public float time;
    public float offDuration = 1.0f;
    private float destoryTime;
    public float destoryTimeDurtation =30f; //30f
    public float getStarDurationColorArea = 70f; // 70f
    private bool isDraw;
    private float maxPercentage =100f;
    private float amount = 0.25f;

    private void Awake()
    {
        inkAreaChecker = GetComponent<InkAreaChecker>();
        UiGameManager.instance.IncreasBar = amount;
    }

    private void Update()
    {
        destoryTime += Time.deltaTime;
        if (Time.time>time+offDuration )
        {
            time = Time.time;
            cubeImage.enabled = isDraw;
            isDraw = !isDraw;
            time = 0f;
        }
        if(destoryTime >=destoryTimeDurtation)
        {
            Debug.Log("Destory");
            var result = inkAreaChecker.GetColorRatio()* maxPercentage;
            if(result >= getStarDurationColorArea)
            {
                Debug.Log("��Ÿ ŉ�� ����");
                UiGameManager.instance.InkAreaChecking =true;
                UiGameManager.instance.FloatInkAreaCheckingClearBarAmountIncrease(amount);
                
            } // �׽�Ʈ�� �ϴ� ������ ����
           // Debug.Log(result);
            Destroy(gameObject);
        }
    }
}
