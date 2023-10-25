using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var message =string.Empty;
        foreach(var touch in Input.touches)
        {
            //message += "touchuid" + touch.fingerid + "\nphase" + touch.phase + "\n";
            //message += "position" + touch.position + "\n";
            //message += "delta pos" + touch.deltaposition + "\n";

            //switch(touch.phase)
            //{
            //    case touchphase.began:
            //        break;
            //    case touchphase.moved:
            //        break;
            //        case touchphase.stationary: break;  
            //        case touchphase.ended:  break;
            //        case touchphase.canceled: break;    
            //}

            message = "test";
        }
        message += "\n";

        text.text = message;    
    }
}
