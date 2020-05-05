using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
//objects
    public new Transform camera;
    protected CharacterController jones;

    //Camera
    private Vector3 camF;
    private Vector3 camR;

    //User input
    protected Vector2 input;

    //Physics
    private Vector3 intent;
    protected Vector3 velocity;
    private Vector3 velocityXZ;

    // player's top speed when moving
    protected float speed = 5;

    // rate of acceleration and deceleration when
    // beginning and ending movement is based on this value
    private float accel = 5;

    //rate of rotation
    private float turnSpeed = 20;

    //Gravity
    private float grav = 9.81f;
    private bool grounded = false;
    public float halfPlayerHeight = 1.1f;

    protected void PlayerStart()
    {
        jones = GetComponent<CharacterController>();
    }
    
    
    protected virtual void GetInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        input = Vector2.ClampMagnitude(input, 1);
    }

    protected void CalculateCamera()
    {
        camF = camera.forward;
        camR = camera.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    protected void CalculateGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position+Vector3.up*0.1f, -Vector3.up, out hit, halfPlayerHeight))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        Debug.DrawRay(transform.position+Vector3.up*0.1f, -Vector3.up *hit.distance, Color.yellow);
        //Debug.Log($"Grounded is {grounded}");
    }

    protected void MovePlane()
    {
        intent = camF * input.y + camR * input.x;

        if(input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * input.magnitude * speed, accel*Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    protected void Gravity()
    {
        if (grounded)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10, 10);
    }

    protected void Jump()
    {
        if(grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = 6;
                //14 degrees is highest angle we can still jump on slopes
                //issue with raycast length in CalculateGround
                //doesnt work when descending slopes
            }
        }
    }
}
