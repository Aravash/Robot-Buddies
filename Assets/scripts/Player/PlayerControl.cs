using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Player
{
    public GameObject validBuildBot;
    public GameObject invalidBuildBot;
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

        CalculateGround();
        
        Gravity();

        jones.Move(velocity * Time.deltaTime);
    }

    void StateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (activePlayer)
            {
                activePlayer = false;
                validBuildBot.SetActive(false);
                invalidBuildBot.SetActive(false);
            }
            else
            {
                activePlayer = true;
            }
        }
    }
    
    /*
     * render a hologram robot if you are looking at
     * a surface you can build on
     */
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
            buildLoc = hit.point;
            if (hit.transform.CompareTag("worldBuildable"))
            {
                canBuild = true;
                invalidBuildBot.SetActive(false);
                validBuildBot.SetActive(true);
                validBuildBot.transform.position = hit.point;
            }
            else
            {
                canBuild = false;
                validBuildBot.SetActive(false);
                invalidBuildBot.SetActive(true);
                invalidBuildBot.transform.position = hit.point;
            }
            Debug.DrawRay(camera.transform.position, camera.forward * hit.distance, Color.yellow);
        }
        else
        {
            validBuildBot.SetActive(false);
            invalidBuildBot.SetActive(false);
        }
    }

    /*
     * If you can build, build a robot to that location
     */
    void BuildBot()
    {
        if (!canBuild) return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Build Button press");
            foreach (GameObject bot in robot)
            {
                PlayerControlNoJump script = bot.GetComponent<PlayerControlNoJump>();
                if (!script.IsDisabled())
                {
                    continue;
                }
                script.EnableBot(buildLoc, validBuildBot.transform.rotation);
                break;
            }
        }
    }

    /*
     * destroy a robot you are looking at
     */
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
                    hit.transform.gameObject.GetComponent<PlayerControlNoJump>().DisableBot();
                }
            }
        }
    }
}