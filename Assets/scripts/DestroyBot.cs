using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBot : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerManager.instance.DisableRobot(i);
                PlayerManager.instance.PlaceRobot(i, Vector3.zero, Quaternion.identity);
            }
        }
        else if (other.CompareTag("robot"))
        {
            Debug.Log("Bot entered destroyZone");
            other.GetComponent<PlayerControlNoJump>().DisableBot();
            int robot_number = other.GetComponent<RobotController>().GetIndexPosition();
            PlayerManager.instance.DisableRobot(robot_number);
            PlayerManager.instance.PlaceRobot(robot_number, Vector3.zero, Quaternion.identity);
        }
    }
}
