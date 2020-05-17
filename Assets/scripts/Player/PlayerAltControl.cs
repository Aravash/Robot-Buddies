using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAltControl : Player
{
    public Transform orbSpawnPoint;
    public GameObject Orb;
    public PlayerControlNoJump[] robot;
    private bool activePlayer = true;

    void Start()
    {
        PlayerStart();
    }
    void Update()
    {
        StateSwitch();
        
        //below two MUST be before Jump() for jumping to work
        CalculateGround();
            
        Gravity();
        
        if (activePlayer)
        {
            GetInput();

            CalculateCamera();

            MovePlane();

            Jump();

            BuildBot();

            BreakBot();
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }
        
        jones.Move(velocity * Time.deltaTime);
    }

    void StateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            activePlayer = !activePlayer;
        }
    }

    /*
     * If you can build, build a robot to that location
     */
    void BuildBot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            //invert layer bitmask
            layerMask = ~layerMask;

            if (Physics.Raycast(camera.transform.position, camera.forward, 1f, layerMask)) return;
            
            Debug.Log("Hit Nothing, building bot");
            GameObject obj = Instantiate(Orb, orbSpawnPoint.position, orbSpawnPoint.rotation);
            obj.GetComponent<Orb>().SetBots(ref robot);
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
