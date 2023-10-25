using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MutilTouchManager : MonoBehaviour
{
    public bool IsTouching {  get; private set; }
    public float ZoomInch { get; private set; }   // �� �� �ƿ�

    public float minZoomInch = 0.2f;
    public float maxZoomInch = 0.5f;

    private float minZoomPixel;
    private float maxZoomPixel;

    private List<int> fingerIdList=new List<int>();
    //���� ���ξƿ� �Ʒ��� �ϳ��� ��ġ �����ϰ���
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
                    //    primaryFingerId = touch.fingerId; //�̷����Ͽ� ���� ���۽�Ű�� ��������
                    //}
                    fingerIdList.Add(touch.fingerId);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary: 
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    //if(primaryFingerId == touch.fingerId)
                    //    primaryFingerId = int.MinValue; // �ϳ��� �ԷµǴ��ڵ� �Լ� ��������
                    fingerIdList.Remove(touch.fingerId);
                    break;
            }
           
        }

        
        
        UpdateZoomInOut();

//#if UNITY_EDITOR || UNITY_STANDALONE 

//        //������ ��� : ���ͷ� ����
//#elif UNITY_ANDROID || UNITY_IOS
        
//        // ����Ͽ����� ����
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

// ��ġ ?UI ����
//if(Input.GetMouseButtonDown(0))
//{
//    if (EventSystem.current.IsPointerOverGameObject())
//    {
//        Debug.Log("Click");
//    }
//}// ���콺 �Է����� �������� �����°�

////�����ε��Ȱ�
//if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
//{
//// �׽�Ʈ �ʿ�
//}