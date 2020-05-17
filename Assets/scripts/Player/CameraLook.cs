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
    public float topicHeight = .5f;
    public Vector2 input;
    public Animator codecAnim;
    private bool playerActive = true;
    public GameObject[] robots;
    public Camera codecCam;
    
    //how many degrees to rotate per second
    public float rotateSpeed = 45;

    private static readonly int IsEquipped = Animator.StringToHash("isEquipped");

    //maybe want to move cursor stuff and mouse raycasts to a new script on the codec/robot controller obj?
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
    
    /*
     * change player state between using codec/robot controller
     * to moving and placing/picking up bots
     */
    void StateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (playerActive)
            {
                playerActive = false;
                codecAnim.SetBool(IsEquipped, true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                playerActive = true;
                codecAnim.SetBool(IsEquipped, false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    /*
     * pushing buttons on the codec. only supports the two turn buttons
     * Should also add ability to swap screens into the main view by clicking on them
     */
    void PushButton()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            int layerMask = 1 << 10;
            RaycastHit hit;
            Ray ray = codecCam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.transform.gameObject.CompareTag("leftButton"))
                {
                    RotateBot(-rotateSpeed);
                }
                else if (hit.transform.gameObject.CompareTag("rightButton"))
                {
                    RotateBot(rotateSpeed);
                }
            }
        }
    }

    /*
     * rotates the robots on the Y axis.
     * change - the degree by which to rotate
     */
    void RotateBot(float change)
    {
        foreach (GameObject bot in robots)
        {
            if (!bot.activeSelf) continue;
            //to add two quaternions together you multiply them. I say frick to quaternions
            bot.transform.Rotate(0, change * Time.deltaTime, 0);
        }
    }
    
    
    //take input from both horizontal and vertical, X and Z in the world and make it input.
    //clamp input to maximum value of 1
    void GetInput()
    {
        input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        input = Vector2.ClampMagnitude(input, 1);
    }

    /*
     * moves camera based on input variable
     */
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