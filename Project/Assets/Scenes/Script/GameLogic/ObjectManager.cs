using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> colorObjectPrefab;
    private int currentColorObject;

    public List<GameObject> makeColorObjectPrefab;

    public List<Transform> objectPosition;
    private int currentobjectPosition;

    public List<Transform> spwanPosition;
    private int currentSpwanPosition;
    
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1)) 
            ChangeColorObject();

        if (Input.GetKeyUp(KeyCode.Alpha2))
            ChanageSpwanPosition();

        if (Input.GetKeyDown(KeyCode.Return))
            MakeColorObject();

        if (Input.GetKeyDown(KeyCode.Alpha0))
            MoveColorObject();
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
        var makingObj = Instantiate(colorObjectPrefab[currentColorObject], spwanPosition[currentSpwanPosition]);
        makeColorObjectPrefab.Add(makingObj);
    }

    void MoveColorObject()
    {

    }
}
