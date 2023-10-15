using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectManager : MonoBehaviour
{
    public List<GameObject> prefabObjects;
    private List<GameObject> makePrefabObjects;
    public List<Transform> spwanPositions;
    public List<Transform> movePositions;

    private float makeTime;
    public float makeTimeDuration = 10f;
    public bool isMakeGameObject;
    private float donMakeTime;
    private float donMakeTimeDuration = 70f;

    public void Awake()
    {
        makePrefabObjects = new List<GameObject>();
    }

    public void Start()
    {
        MakeGameObject();
    }
    public void Update()
    {
        donMakeTime += Time.deltaTime;
        makeTime += Time.deltaTime;
        if(makeTime > makeTimeDuration && donMakeTime<=donMakeTimeDuration)
        {
            MakeGameObject();
            makeTime = 0f;
        }
        if(isMakeGameObject)
        {
            isMakeGameObject = false;
            MakeGameObject();
        }
    }

    public void MakeGameObject()
    {
        int randomMakeObejct = UnityEngine.Random.Range(0,prefabObjects.Count);
        int randomSpwanPositionIndex = UnityEngine.Random.Range(0, spwanPositions.Count);


        var makeObject = Instantiate(prefabObjects[randomMakeObejct]);
        makeObject.transform.position = spwanPositions[randomSpwanPositionIndex].transform.position;

        var makeObjectSetting = makeObject.GetComponent<MovingGameObject>();
        makeObjectSetting.MovePositionSetting(movePositions);
        makeObjectSetting.GameLogicMovingSetting();

        makePrefabObjects.Add(makeObject);

#if UNITY_EDITOR
        UnityEditor.Selection.activeGameObject = makeObject;
#endif

    }
}
