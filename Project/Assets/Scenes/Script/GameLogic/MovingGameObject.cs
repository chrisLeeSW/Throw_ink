using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingGameObject : MonoBehaviour
{
    private bool isStartHold;
    private bool isStarting;
    private bool isLoopMode ;
    private bool isRandomMode;


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

    private float holdTime;
    private float moveSpeed=3f;
    private float rotateSpeed=0.5f;   
    private void Awake()
    {
        movingTargets = new List<Transform>();

        isStarting = true;
        isLoopMode = true;
        isRandomMode = true;

        moveSpeed = OnGameData.instance.GetStageObjectMoveSpeed(OnGameData.instance.CurrentData);
        holdTime = OnGameData.instance.GetStageObjectHoldTime(OnGameData.instance.CurrentData);
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
        if (Objectdirection == Vector3.zero)
        {
            Objectdirection = Vector3.forward; 
        }
        else
        {
            Objectdirection.Normalize();
        }
       // Objectdirection.Normalize();
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
            //transform.position = Vector3.Lerp(startMovePosition, movingTargets[currentPosIndex].transform.position, rotate);    

            Vector3 newPosition = Vector3.Lerp(startMovePosition, movingTargets[currentPosIndex].transform.position, rotate);
            if (!float.IsNaN(newPosition.x) && !float.IsNaN(newPosition.y) && !float.IsNaN(newPosition.z))
            {
                transform.position = newPosition;
            }

            if (Vector3.Distance(transform.position, movingTargets[currentPosIndex].transform.position) < 0.1f)
            {
                break;
            }
            if (Vector3.Distance(transform.position, movingTargets[currentPosIndex].transform.position) < 0.1f)
            {
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
        Vector3 randomRotation = new Vector3(UnityEngine.Random.Range(0f, 360f),
                                     UnityEngine.Random.Range(0f, 360f),
                                     UnityEngine.Random.Range(0f, 360f));
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(randomRotation);
        float rotationTime = 0.0f;

        while (rotationTime <= holdTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationTime / holdTime);
            rotationTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        // yield return new WaitForSeconds(holdTime);
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
