using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    [Tooltip("The player does not start with the codec")]
    public bool has_codec = false;
    
    [Tooltip("If using the codec, the robots are moved, else the player is moved")]
    public bool using_codec = false;

    [SerializeField]
    private GameObject[] Robots = new GameObject[4];

    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!has_codec) return;

        if (Input.GetButtonDown("ToggleCodecView"))
        {
            using_codec = !using_codec; 
        }
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
