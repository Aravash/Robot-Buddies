using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Player
{
    public GameObject cube;
    private bool canBuild = false;
    private Vector3 buildLoc;
    public GameObject[] robot;
    private bool activePlayer = true;

    void Start()
    {
        PlayerStart();
    }
    void Update()
    {
        StateSwitch();
        if (activePlayer)
        {
            GetInput();

            CalculateCamera();

            CalculateGround();

            MovePlane();

            Jump();

            LookSpot();

            BuildBot();

            BreakBot();
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }

        Gravity();
            
        jones.Move(velocity * Time.deltaTime);
    }

    void StateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            activePlayer = !activePlayer;
        }
    }
    
    void LookSpot()
    {
        //TODO:: compartmentalize raycast into function that takes in raycast parameters
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        
        //invert layer bitmask
        layerMask = ~layerMask;
    
        RaycastHit hit;
        // Does the ray intersect any objects except the player
        if (Physics.Raycast(camera.transform.position, camera.forward, out hit, 5f, layerMask))
        {
            cube.SetActive(true);
            cube.transform.position = hit.point;
            buildLoc = hit.point;
            if (hit.transform.CompareTag("worldBuildable"))
            {
                canBuild = true;
            }
            else
            {
                canBuild = false;
            }
            Debug.DrawRay(camera.transform.position, camera.forward * hit.distance, Color.yellow);
        }
        else cube.SetActive(false);
    }

    void BuildBot()
    {
        if (!canBuild) return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            foreach (GameObject bot in robot)
            {
                if (bot.activeSelf)
                {
                    continue;
                }
                Debug.Log("BOT deployed.");
                bot.SetActive(true);
                bot.transform.position = buildLoc;
                bot.transform.rotation = cube.transform.rotation;
                break;
            }
        }
    }

    void BreakBot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
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
                    hit.transform.gameObject.SetActive(false);
                    Debug.Log("BOT removed.");
                }
            }
        }
    }
}