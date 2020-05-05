using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlNoJump : Player
{
    private Animator animator;
    private static readonly int Moving = Animator.StringToHash("moving");
    private bool activeBot = false;

    void Start()
    {
        PlayerStart();
        animator = GetComponent<Animator>();
        speed = 2;
    }
    void Update()
    {
        StateSwitch();
        if(activeBot)
        {


            GetInput();

            Animate();

            CalculateCamera();

            CalculateGround();

            MovePlane();
            
            Gravity();
            
            jones.Move(velocity * Time.deltaTime);
            
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }
    }

    void StateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            activeBot = !activeBot;
            animator.SetBool(Moving, false);
        }
    }
    
    protected override void GetInput()
    {
        input = new Vector2(0, Input.GetAxis("BotVertical"));

        input = Vector2.ClampMagnitude(input, 1);
    }
    
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
}
