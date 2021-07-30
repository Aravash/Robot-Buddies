using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("robot"))
        {
            int robot_number = other.GetComponent<RobotController>().GetIndexPosition();
            PlayerManager.instance.DisableRobot(robot_number);
            PlayerManager.instance.PlaceRobot(robot_number, Vector3.zero, Quaternion.identity);
        }
    }
}
