using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;





public class ObjectManager : MonoBehaviour
{


    public List<GameObject> colorObjectPrefab;
    private int currentColorObject;

    private List<GameObject> makeColorObjectPrefab;
    private int currentMakeColorObjectpreFab;

    public List<Transform> objectPosition;
    private int currentobjectPosition;

    public List<Transform> spwanPosition;
    private int currentSpwanPosition;


    private Vector3 objDirection;
    private Quaternion startRot;
    private Quaternion endRot;

    private float startTime;
    private float newTimeSpeed;
    public float moveSpeed = 3f;
    float roate;
    private float journeyLength;
    public float rotateSpeed = 0.25f;
    private Vector3 startMovePosition;

    public bool isRandomMode;
    public bool isLoopMode;
    public float holdTime;
    public bool isMoving;

    public bool isStartHold;
    private void Awake()
    {
        makeColorObjectPrefab = new List<GameObject>();

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            ChangeColorObject();

        if (Input.GetKeyUp(KeyCode.Alpha2))
            ChanageSpwanPosition();

        if (Input.GetKeyDown(KeyCode.Return))
            MakeColorObject();

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            DirectionSettingColorObject();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && !isLoopMode)
        {
            isLoopMode = true;
            if (isStartHold) StartCoroutine(StartHoldTime());
            else DirectionSettingColorObject();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            isLoopMode = false;
        }

    }
  
   
    void ChangeColorObject()
    {
        currentColorObject++;
        if (currentColorObject >= colorObjectPrefab.Count)
            currentColorObject = 0;
    }

    void ChanageSpwanPosition()
    {
        currentSpwanPosition++;

        if (currentSpwanPosition >= spwanPosition.Count)
            currentSpwanPosition = 0;

    }

    void MakeColorObject()
    {
        var makingObj = Instantiate(colorObjectPrefab[currentColorObject]);
        makingObj.transform.position = spwanPosition[currentSpwanPosition].transform.position;
        makeColorObjectPrefab.Add(makingObj);
    }

    void DirectionSettingColorObject()
    { 
        int currentPosIndex = 0;
        if (isRandomMode)
            currentPosIndex = UnityEngine.Random.Range(0, objectPosition.Count);
        else if (!isRandomMode)
            currentPosIndex = currentobjectPosition;
        isMoving = true;

        objDirection = makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position - objectPosition[currentPosIndex].transform.position;
        objDirection.Normalize();
        startRot = makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.rotation;
        endRot = Quaternion.LookRotation(objDirection);
        startMovePosition = makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position, objectPosition[currentPosIndex].transform.position);

        StartCoroutine(MoveColorObject(currentPosIndex));
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

    IEnumerator MoveColorObject(int currentPosIndex)
    {
        while (true)
        {
            yield return null;
            newTimeSpeed = (Time.time - startTime) * moveSpeed;
            roate = newTimeSpeed / journeyLength;
            makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.rotation = Quaternion.Slerp(startRot, endRot, roate * rotateSpeed);
            makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position = Vector3.Lerp(startMovePosition, objectPosition[currentPosIndex].transform.position, roate);

            if (Vector3.Distance(makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position, objectPosition[currentPosIndex].transform.position) < 0.1f)
            {

                isMoving = false;
                break;
            }
        }
        yield return StartCoroutine(HoldTime());
        currentobjectPosition++;
        if (currentobjectPosition >= objectPosition.Count)
        {
            currentobjectPosition = 0;
        }
        if (isLoopMode)
        {
            DirectionSettingColorObject();
        }
    }
}
