using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject otherTeleporter;
    private Transform targetTelePoint;
    [SerializeField] private Transform exitPoint;
    
    // Start is called before the first frame update
    private void Start()
    {
        //exitPoint = transform.GetChild(0).transform;
        targetTelePoint = otherTeleporter.GetComponent<Teleporter>().GetExitPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("robot")) return;
        Debug.Log("teleporting bot");
        PlayerControlNoJump script = other.GetComponent<PlayerControlNoJump>();
        script.DisableBot();
        script.EnableBot(targetTelePoint.position, targetTelePoint.rotation);
        //need to be able to set active of bot after 1 frame has passed.
    }

    public Transform GetExitPoint()
    {
        return exitPoint;
    }
}
