using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MutilTouchManager : MonoBehaviour
{
    public bool IsTouching {  get; private set; }
    public float ZoomInch { get; private set; }   // 줌 인 아웃

    public float minZoomInch = 0.2f;
    public float maxZoomInch = 0.5f;

    private float minZoomPixel;
    private float maxZoomPixel;

    private List<int> fingerIdList=new List<int>();
    //위는 줌인아웃 아래는 하나만 터치 가능하게함
    private int primaryFingerId = int.MinValue;
    private void Awake()
    {
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);

        minZoomPixel = minZoomInch * Screen.dpi;
        maxZoomPixel = maxZoomInch * Screen.dpi;
        
        Debug.Log(Screen.dpi);
    }
    void Update()
    {
        foreach(var touch in Input.touches)
        {
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    //if(fingerIdList.Count== 0 && primaryFingerId == int.MinValue)
                    //{
                    //    primaryFingerId = touch.fingerId; //이렇게하여 이제 동작시키게 만들어야함
                    //}
                    fingerIdList.Add(touch.fingerId);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary: 
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    //if(primaryFingerId == touch.fingerId)
                    //    primaryFingerId = int.MinValue; // 하나만 입력되는코드 함수 만들어야함
                    fingerIdList.Remove(touch.fingerId);
                    break;
            }
           
        }

        
        
        UpdateZoomInOut();

//#if UNITY_EDITOR || UNITY_STANDALONE 

//        //에디터 기능 : 컴터로 동작
//#elif UNITY_ANDROID || UNITY_IOS
        
//        // 모바일에서만 동작
//#endif
    }

    public void UpdateZoomInOut()
    {
        if (fingerIdList.Count >= 2)
        {
            Vector2[] prevTouchPos = new Vector2[2];
            Vector2[] currentTouchPos = new Vector2[2];
            for (int i = 0; i < 2; i++)
            {
                var touch = Array.Find(Input.touches,
                    x => x.fingerId == fingerIdList[i]);
                currentTouchPos[i] = touch.position;
                prevTouchPos[i] = touch.position - touch.deltaPosition;
            }

            var prevFreamDist = Vector2.Distance(prevTouchPos[0], prevTouchPos[1]);
            var currentFrameDist = Vector2.Distance(currentTouchPos[0], currentTouchPos[1]);

            var distancePixel = prevFreamDist - currentFrameDist;
            // Debug.Log(currentFrameDist - prevFreamDist);

            ZoomInch = distancePixel / Screen.dpi;
        }
    }
}

// 터치 ?UI 관련
//if(Input.GetMouseButtonDown(0))
//{
//    if (EventSystem.current.IsPointerOverGameObject())
//    {
//        Debug.Log("Click");
//    }
//}// 마우스 입력으로 기준으로 나오는거

////오버로딩된거
//if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
//{
//// 테스트 필요
//}