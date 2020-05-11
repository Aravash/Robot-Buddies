using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBot : MonoBehaviour
{
    
    public PlayerControlNoJump[] robots;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] botObj = GameObject.FindGameObjectsWithTag("robot");
        for (int i = 0; i < 4; i++)
        {
            robots[i] = botObj[i].GetComponent<PlayerControlNoJump>();
            Debug.Log("Assigned a robot's script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered destroyZone");
            foreach (PlayerControlNoJump robot in robots)
            {
                robot.DisableBot();
            }
        }
        else if (other.CompareTag("robot"))
        {
            Debug.Log("Bot entered destroyZone");
            other.GetComponent<PlayerControlNoJump>().DisableBot();
        }
    }
}
