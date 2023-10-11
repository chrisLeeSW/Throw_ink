using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectManager : MonoBehaviour
{
    public List<GameObject> prefabObjects;
    private List<GameObject> makePrefabObjects;
    public List<Transform> spwanPositions;
    public List<Transform> movePositions;

    public bool isMakeGameObject;

    public void Awake()
    {
        makePrefabObjects = new List<GameObject>();
    }
    public void Update()
    {
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

        makePrefabObjects.Add(makeObject);

#if UNITY_EDITOR
        UnityEditor.Selection.activeGameObject = makeObject;
#endif

    }
}
