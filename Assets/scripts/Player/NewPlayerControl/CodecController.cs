using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodecController : MonoBehaviour
{
    public Renderer[] codecCams;
    public Material[] robotCam;
    public Renderer[] robotLights = new Renderer[4];
    public Material offScreen;
    public Material[] ToggleLights = new Material[2];

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerManager.instance.IsCurrentRobotEnabled(i))
            {
                codecCams[i].material = robotCam[i];
                robotLights[i].material = ToggleLights[1];
            }
            else
            {
                codecCams[i].material = offScreen;
                robotLights[i].material = ToggleLights[0];
            }
        }
    }
}
