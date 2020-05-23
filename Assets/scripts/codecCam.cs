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

    public void SwapScreen(GameObject screen)
    {
        Renderer render = screen.GetComponent<Renderer>();
        
        if (render.material == offScreen) return;
        Debug.Log("material is not offScreen");
        Material mat = codecCams[0].material;
        codecCams[0].material = render.material;
        render.material = mat;
    }
}
