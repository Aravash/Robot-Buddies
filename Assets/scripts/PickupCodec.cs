using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCodec : MonoBehaviour
{
    public CameraLook cam;
    public GameObject codecObj;
    
    public PlayerControlNoJump[] robots;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] botObj = GameObject.FindGameObjectsWithTag("robot");
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("iterated once");
            robots[i] = botObj[i].GetComponent<PlayerControlNoJump>();
            //Debug.Log("Assigned a robot's script");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAltControl>().hasCodec = true;
            cam.hasCodec = true;
            codecObj.SetActive(true);
            
            foreach (PlayerControlNoJump robot in robots)
            {
                robot.playerHasCodec = true;
            }

            Destroy(gameObject);
        }
    }
}
