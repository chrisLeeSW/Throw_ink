using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSubController : MonoBehaviour
{
    public Image cubeImage;

    public float time;
    public float offDuration = 1.0f;
    public float destoryTime;
    public float destoryTimeDurtation =30f;
    private bool isDraw;

    private void Update()
    {
        
        if(Time.time>time+offDuration )
        {
            time = Time.time;
            cubeImage.enabled = isDraw;
            isDraw = !isDraw;
            time = 0f;
        }
        if( Time.time>= destoryTime + destoryTimeDurtation)
        {
            Debug.Log("Destory");
            Destroy(gameObject);
        }
    }
}
