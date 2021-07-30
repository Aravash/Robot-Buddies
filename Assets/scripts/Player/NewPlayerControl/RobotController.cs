using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : Player
{
    private Animator animator;
    private static readonly int Moving = Animator.StringToHash("moving");
    private int index_position;

    void Start()
    {
        PlayerStart();
        animator = GetComponent<Animator>();
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        Animate();

        CalculateCamera();

        CalculateGround();

        MovePlane();
            
        Gravity();
        
        if (PlayerManager.instance.using_codec)
        {
            GetInput();

            jones.Move(velocity * Time.deltaTime);
        }
        else
        {
            input.y = 0;
            velocity = new Vector3(0, velocity.y, 0);
        }
    }
    
    /*
    * get input only on the z axis of the world.
    */
    protected override void GetInput()
    {
        input = new Vector2(0, Input.GetAxis("BotVertical"));

        input = Vector2.ClampMagnitude(input, 1);
    }
    
    /*
    * animate the bots when they are moving
    */
    private void Animate()
    {
        if (input.y > 0)
        {
            animator.SetBool(Moving, true);
        }
        else
        {
            animator.SetBool(Moving, false);
        }
    }

    public void SetIndexPosition(int newpos)
    {
        index_position = newpos;
    }
    
    public int GetIndexPosition()
    {
        return index_position;
    }
}
