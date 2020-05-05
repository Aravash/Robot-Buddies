using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    // in this case topic is the player
    public Transform topic;
    private float heading = 0;
    private float tilt = 0;
    private float camDist = 0;
    private float currentDist = 0;
    private float topicHeight = 1;
    public Vector2 input;
    public GameObject codec;
    private bool playerActive = true;
    public GameObject[] robots;

    [HideInInspector]
    public bool in2D = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        codec.SetActive(false);
        currentDist = camDist;
    }

    private void Update()
    {
        StateSwitch();

        if (!playerActive)
        {
            PushButton();
        }
        else
        {
            GetInput();
        }
    }

    private void LateUpdate()
    {
        if (!playerActive) return;
        
        MoveCam();
    }
    // using LateUpdate instead of update means camera position 
    // will always change after the player position changes.
    // this avoids potential desync on lower framerates
    
    void StateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (playerActive)
            {
                playerActive = false;
                codec.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                playerActive = true;
                codec.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void PushButton()
    {
        Debug.Log("Polling PushButton");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Mouse0 pressed");
            int layerMask = 1 << 10;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.transform.gameObject.CompareTag("leftButton"))
                {
                    Debug.Log("Selected Left Component");
                    RotateBot(-15);
                }
                else if (hit.transform.gameObject.CompareTag("rightButton"))
                {
                    Debug.Log("Selected Right Component");
                    RotateBot(+15);
                }
            }
        }
    }

    void RotateBot(float change)
    {
        foreach (GameObject bot in robots)
        {
            if (!bot.activeSelf) continue;
            //to add two quaternions together you multiply them. I say frick to quaternions
            bot.transform.Rotate(0, change, 0);
        }
    }
    
    
    //take input from both horizontal and vertical, X and Z in the world and make it input.
    //clamp input to maximum value of 1
    void GetInput()
    {
        input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        input = Vector2.ClampMagnitude(input, 1);
    }

    private void MoveCam()
    {
        if (input.magnitude > 0)
        {
            tilt += Input.GetAxis("Mouse Y") * Time.deltaTime * 180;

            heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;

            tilt = Mathf.Clamp(tilt, -90, 90);
        }

        transform.rotation = Quaternion.Euler(tilt, heading, 0);

        transform.position = topic.position - transform.forward * currentDist + Vector3.up * topicHeight;
    }
}