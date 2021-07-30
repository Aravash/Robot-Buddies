using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public Transform orbSpawnPoint;
    public GameObject Orb;

    private void Start()
    {
        PlayerStart();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateGround();
            
        Gravity();
        
        if (!PlayerManager.instance.using_codec)
        {
            GetInput();

            CalculateCamera();

            MovePlane();

            Jump();
            
            if (PlayerManager.instance.has_codec)
            {
                BuildBot();

                BreakBot();
            }
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }
        
        jones.Move(velocity * Time.deltaTime);
    }
    
    //if you have space in front of you, throw a robot down.
    void BuildBot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerManager.instance.GetFirstInactiveRobotIndex() == -1) return;
            
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;
            //invert layer bitmask
            layerMask = ~layerMask;

            if (Physics.Raycast(camera.transform.position, camera.forward, 1f, layerMask)) return;
            
            GameObject obj = Instantiate(Orb, orbSpawnPoint.position, orbSpawnPoint.rotation);
            //obj.GetComponent<Orb>().SetBots(ref robot);
        }
    }
    
    void BreakBot()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;
            //invert layer bitmask
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects except the player
            if (Physics.Raycast(camera.transform.position, camera.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(camera.transform.position, camera.forward * hit.distance, Color.red);
                if (hit.transform.CompareTag("robot"))
                {
                    int robot_number = hit.transform.GetComponent<RobotController>().GetIndexPosition();
                    PlayerManager.instance.DisableRobot(robot_number);
                    PlayerManager.instance.PlaceRobot(robot_number, Vector3.zero, Quaternion.identity);
                }
            }
        }
    }
    
    
}
