using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockageController : MonoBehaviour
{
    public GameObject[] redlights;
    public static bool canGo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < redlights.Length; i++)
        {
           if (!redlights[i].activeInHierarchy)
            {
                //canGo = false;
                gameObject.tag = "Blockage";
                canGo = false;
                break;
            } 
           else //canGo = true;
            {
                gameObject.tag = "Green Light";
                canGo=true;
            }
        }

 
    }
}
