using System;
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

    private GameObject bigScreen;

    private void Awake()
    {
        bigScreen = codecCams[0].gameObject;
    }

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
    
    public void swapRobotCamArrayPositions(int Pos1, int Pos2)
    {
        Material temp = robotCam[Pos1];
        robotCam[Pos1] = robotCam[Pos2];
        robotCam[Pos2] = temp;
        Debug.Log("swapping screen");
    }

    public void swapWithMainCamera(GameObject otherCamera)
    {
        string other_tag = otherCamera.tag;
        otherCamera.tag = bigScreen.tag;
        bigScreen.tag = other_tag;
        
        Vector3 other_position = otherCamera.transform.position;
        otherCamera.transform.position = bigScreen.transform.position;
        bigScreen.transform.position = other_position;
        
        Vector3 other_local_scale = otherCamera.transform.localScale;
        otherCamera.transform.localScale = bigScreen.transform.localScale;
        bigScreen.transform.localScale = other_local_scale;

        bigScreen = otherCamera;
    }
}
