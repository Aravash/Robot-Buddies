using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployBot : MonoBehaviour
{
    
    public float force = 2f;
    public float halfHeight = .3f;
    private Rigidbody rb;
    private int my_robot_index;
    int layerMask = 1 << 12;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        layerMask = ~layerMask;
        my_robot_index = PlayerManager.instance.GetFirstInactiveRobotIndex();
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchingGround())
        {
            CreateRobot();
            Destroy(gameObject);
        }
    }

    private bool TouchingGround()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -Vector3.up, out hit, halfHeight, layerMask);
    }

    private void CreateRobot()
    {
        PlayerManager.instance.PlaceRobot(my_robot_index, transform.position, transform.rotation);
        PlayerManager.instance.EnableRobot(my_robot_index);
    }
}
