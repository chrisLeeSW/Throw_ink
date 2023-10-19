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
    private float destoryTimeDurtation=30f ; //30f
    private float getStarDurationColorArea=60f; // 70f
    private bool isDraw;
    private float maxPercentage =100f;
    private float amount = 0.25f;

    public ParticleSystem particle;
    private void Awake()
    {
        inkAreaChecker = GetComponent<InkAreaChecker>();
        UiGameManager.instance.IncreasBar = amount;

        destoryTimeDurtation = OnGameData.instance.GetStageObjectLife(OnGameData.instance.CurrentData);
        getStarDurationColorArea= OnGameData.instance.GetStageObjectAreaCondition(OnGameData.instance.CurrentData);
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
            particle.Play();
            var result = inkAreaChecker.GetColorRatio()* maxPercentage;
            if(result >= getStarDurationColorArea)
            {
                UiGameManager.instance.InkAreaChecking =true;
                UiGameManager.instance.FloatInkAreaCheckingClearBarAmountIncrease();
            } 
           // Debug.Log(result);
            Destroy(gameObject);
        }
    }
}
