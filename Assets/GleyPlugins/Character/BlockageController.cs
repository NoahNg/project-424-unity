using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockageController : MonoBehaviour
{
    public GameObject[] redlights; //this object array includes all the red lights of the traffic lights
    public static bool canGo = false;

    // Update is called once per frame
    void Update()
    {
        //run through the array of all red light objects
        for (int i = 0; i < redlights.Length; i++)
        {
            //if not all the red lights are active, then pedestrians can't cross
           if (!redlights[i].activeInHierarchy)
            {
                //canGo = false;
                gameObject.tag = "Blockage";
                canGo = false;
                break;
            } 
           else 
            {
                gameObject.tag = "Green Light";
                canGo=true;
            }
        }

 
    }
}
