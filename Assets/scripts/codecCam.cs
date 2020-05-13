using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codecCam : MonoBehaviour
{
    public Renderer[] codecCams;
    public PlayerControlNoJump[] robots;
    public Material[] robotCam;
    public Material offScreen;

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (robots[i].IsDisabled())
            {
                codecCams[i].material = offScreen;
            }
            else
            {
                codecCams[i].material = robotCam[i];
            }
        }
    }
}
