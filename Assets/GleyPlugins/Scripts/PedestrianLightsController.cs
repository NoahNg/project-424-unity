using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianLightsController : MonoBehaviour
{
    public GameObject redLight;
    public GameObject greenLight;

    // Update is called once per frame
    void Update()
    {
        //if all the traffic lights are red, then pedestrian lights are green
        if (BlockageController.canGo == true)
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }

        //if one of the traffic lights is green, then pedestrian lights are red
        if (BlockageController.canGo == false)
        {
            greenLight.SetActive(false);
            redLight.SetActive(true);
        }
    }
}
