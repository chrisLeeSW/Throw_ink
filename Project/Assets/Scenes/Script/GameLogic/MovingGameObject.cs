using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingGameObject : MonoBehaviour
{
    public bool isStartHold;
    public bool isStarting;
    public bool isLoopMode;
    public bool isRandomMode;
    public bool isMoving;

    public List<Transform> movingTargets;
    public int currentMovingTargetsIndex;

    private Vector3 Objectdirection;
    private Vector3 startMovePosition;  
    private float journeyLength;
    private float newTimeSpeed;
    private float startTime;
    private float rotate;
    private Quaternion startRoate;
    private Quaternion endRoate;

    public float holdTime;
    public float moveSpeed=3f;
    public float rotateSpeed=0.5f;   
    private void Awake()
    {
        movingTargets = new List<Transform>();
    }
    private void Update()
    {
        if(isStarting)
        {
            isStarting = false;
            if (isStartHold) StartCoroutine(StartHoldTime());
            else DirectionSettingColorObject();
        }
            
    }

    public void MovePositionSetting(List<Transform> targts)
    {
       for(int i = 0;i < targts.Count; i++)
        {
            movingTargets.Add(targts[i]);
        }
    }

    public void DirectionSettingColorObject()
    {
        int currentPosIndex = 0;
        if (isRandomMode)
            currentPosIndex = UnityEngine.Random.Range(0, movingTargets.Count);
        else
            currentPosIndex = currentMovingTargetsIndex;

        Objectdirection = transform.position - movingTargets[currentPosIndex].transform.position;
        Objectdirection.Normalize();
        startRoate = transform.rotation;
        endRoate = Quaternion.LookRotation(Objectdirection);
        startMovePosition = transform.position;
        journeyLength = Vector3.Distance(transform.position, movingTargets[currentPosIndex].transform.position);
        startTime = Time.time;
        StartCoroutine(MoveColorObject(currentPosIndex));
    }
    IEnumerator MoveColorObject(int currentPosIndex)
    {
        while (true)
        {
            yield return null;
            newTimeSpeed = (Time.time - startTime) * moveSpeed;
            rotate = newTimeSpeed / journeyLength;
            transform.rotation = Quaternion.Slerp(startRoate, endRoate, rotate * rotateSpeed);
            transform.position = Vector3.Lerp(startMovePosition, movingTargets[currentPosIndex].transform.position, rotate);

            if (Vector3.Distance(transform.position, movingTargets[currentPosIndex].transform.position) < 0.1f)
            {

                isMoving = false;
                break;
            }
        }
        yield return StartCoroutine(HoldTime());
        currentMovingTargetsIndex++;
        if (currentMovingTargetsIndex >= movingTargets.Count)
        {
            currentMovingTargetsIndex = 0;
        }
        if (isLoopMode)
        {
            DirectionSettingColorObject();
        }
    }
    IEnumerator StartHoldTime()
    {
        yield return new WaitForSeconds(holdTime);
        DirectionSettingColorObject();
    }
    IEnumerator HoldTime()
    {
        yield return new WaitForSeconds(holdTime);
    }

    public void GameLogicMovingSetting()
    {
        isStarting = true;
        isLoopMode = true;
        if (UnityEngine.Random.Range(0, 2) == 1)
            isRandomMode = true;
        if (UnityEngine.Random.Range(0, 4) == 3)
            isStartHold = true;
    }
}
