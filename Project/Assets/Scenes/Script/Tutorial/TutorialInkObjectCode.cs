using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInkObjectCode : MonoBehaviour
{
    private bool isRoate;
    private float timer;
    private float timerDuration=5f;
    private float moveTime = 3f;
    private float startInkArea;
    private bool isCoilledPlayer;


    private bool isClear;
    private void Update()
    {
        if (!isClear)
        {
            timer += Time.deltaTime;
            if (timer > timerDuration)
            {
                isRoate = true;
                timer = 0;
            }

            if (isRoate)
            {
                isRoate = false;
                StartCoroutine(SpinObject());
            }

            if (isCoilledPlayer)
            {
                startInkArea += Time.deltaTime;
                if (startInkArea > 1.0f)
                {
                    var area = GetComponent<InkAreaChecker>();
                    startInkArea = 0f;
                    var result = area.GetColorRatio() * 100f;
                    TutorailUiManager.instance.SetAreaResult(result);
                    if (result >= 60f)
                    {
                        isClear = true;
                        TutorailUiManager.instance.IsClear = isClear;
                    }

                }
            }
        }
    }
    private IEnumerator SpinObject()
    {
        Vector3 randomRotation = new Vector3(UnityEngine.Random.Range(0f, 360f),
                                     UnityEngine.Random.Range(0f, 360f),
                                     UnityEngine.Random.Range(0f, 360f));
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(randomRotation);
        float rotationTime = 0.0f;

        while (rotationTime <= moveTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationTime / moveTime);
            rotationTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isCoilledPlayer =true;
        }
    }
}
