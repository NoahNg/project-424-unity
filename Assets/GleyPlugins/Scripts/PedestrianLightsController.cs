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
        if (BlockageController.canGo == true)
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }

        if (BlockageController.canGo == false)
        {
            greenLight.SetActive(false);
            redLight.SetActive(true);
        }
    }
}
