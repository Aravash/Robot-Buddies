using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    [Tooltip("The player does not start with the codec")]
    public bool has_codec = false;
    
    [Tooltip("If using the codec, the robots are moved, else the player is moved")]
    public bool using_codec = false;

    public enum UI_state
    {
        OUT_OF_CODEC,
        USING_CODEC,
        ESCAPE_MENU
    }

    public UI_state current_ui_state;

    [SerializeField]
    private GameObject[] Robots = new GameObject[4];

    [SerializeField] private Animator codec_anim;
    private static readonly int IsEquipped = Animator.StringToHash("isEquipped");

    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        int i = 0;
        foreach (var Bot in instance.Robots)
        {
            Bot.GetComponent<RobotController>().SetIndexPosition(i);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleEscapeMenu();
        
        if (!has_codec && current_ui_state != UI_state.ESCAPE_MENU) return;

        if (Input.GetButtonDown("ToggleCodecView"))
        {
            using_codec = !using_codec;
            handleCursorAndUI();
            handleCodec();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (using_codec)
            {
                using_codec = false;
                handleCursorAndUI();
                handleCodec();
            }
        }
    }

    private void handleEscapeMenu()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (current_ui_state == UI_state.ESCAPE_MENU)
            {
                ExitEscapeMenu();
            }
            else
            {
                Time.timeScale = 0;
                current_ui_state = UI_state.ESCAPE_MENU;
                UIToggle.instance.changeUIState(current_ui_state);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void ExitEscapeMenu()
    {
        Time.timeScale = 1;
        current_ui_state = UI_state.OUT_OF_CODEC;
        UIToggle.instance.changeUIState(current_ui_state);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void handleCursorAndUI()
    {
        Debug.Log("just called handlecusorandUI");
        if (using_codec)
        {
            current_ui_state = UI_state.USING_CODEC;
            UIToggle.instance.changeUIState(current_ui_state);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            current_ui_state = UI_state.OUT_OF_CODEC;
            UIToggle.instance.changeUIState(current_ui_state);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void handleCodec()
    {
        if(using_codec) codec_anim.SetBool(IsEquipped, true);
        else codec_anim.SetBool(IsEquipped, false);
    }
    
    //place the robot before enabling it, and disable it before placing it.
    public void PlaceRobot(int robot_number, Vector3 new_location, Quaternion new_rotation)
    {
        instance.Robots[robot_number].transform.position = new_location;
        instance.Robots[robot_number].transform.rotation = new_rotation;
    }

    public void EnableRobot(int robot_number)
    {
        instance.Robots[robot_number].SetActive(true);
    }

    public void DisableRobot(int robot_number)
    {
        instance.Robots[robot_number].SetActive(false);
    }

    public bool IsCurrentRobotEnabled(int robot_number)
    {
        return instance.Robots[robot_number].activeSelf;
    }

    public void rotateRobots(float change)
    {
        foreach (var Bot in instance.Robots)
        {
            if (!Bot.activeSelf) continue;
            Bot.transform.Rotate(0, change * Time.deltaTime, 0);
        }
    }

    public int GetFirstInactiveRobotIndex()
    {
        int i = 0;
        foreach (var Bot in instance.Robots)
        {
            if (!Bot.activeSelf) return i;
            i++;
        }
        return -1;
    }
}
