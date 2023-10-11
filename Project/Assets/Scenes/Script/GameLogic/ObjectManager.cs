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
    private void Awake()
    {
        makeColorObjectPrefab =new List<GameObject>();

    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1)) 
            ChangeColorObject();

        if (Input.GetKeyUp(KeyCode.Alpha2))
            ChanageSpwanPosition();

        if (Input.GetKeyDown(KeyCode.Return))
            MakeColorObject();

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            DirectionSettingColorObject();
        }
    }

    void ChangeColorObject()
    {
        currentColorObject++;
        if(currentColorObject >= colorObjectPrefab.Count)
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
        objDirection = makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position - objectPosition[currentobjectPosition].transform.position ;
        objDirection.Normalize();
        startRot = makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.rotation;
        endRot = Quaternion.LookRotation(objDirection);
        startMovePosition = makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position;
        startTime = Time.time;

        journeyLength = Vector3.Distance(makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position, objectPosition[currentobjectPosition].transform.position);

        StartCoroutine(MoveColorObject());
    }

    IEnumerator MoveColorObject()
    {
        while(true)
        {
            yield return null;
            newTimeSpeed = (Time.time - startTime) * moveSpeed;
            roate = newTimeSpeed / journeyLength;
            makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.rotation = Quaternion.Slerp(startRot, endRot, roate * rotateSpeed);
            makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position = Vector3.Lerp(startMovePosition, objectPosition[currentobjectPosition].transform.position, roate);

            if (Vector3.Distance(makeColorObjectPrefab[currentMakeColorObjectpreFab].transform.position, objectPosition[currentobjectPosition].transform.position) < 0.1f)
            {
                currentobjectPosition++;
                if (currentobjectPosition >= objectPosition.Count)
                    currentobjectPosition = 0;
                break;
            }
        }
        
    }
}
