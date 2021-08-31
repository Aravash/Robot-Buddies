using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRobotBehaviour : Player
{
    private Animator animator;
    private static readonly int Moving = Animator.StringToHash("moving");
    private int index_position;

    [SerializeField] private float max_time_until_next_movement = 4;
    private float time_before_next_movement;

    private action current_action = action.WAITING;
    enum action
    {
        WAITING,
        WALKING,
        ROTATING
    }

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
        
        controlInput();
        
        jones.Move(velocity * Time.deltaTime);
    }
    
    /*
    * get input only on the z axis of the world.
    */
    protected override void GetInput()
    {
        input = new Vector2(0, Input.GetAxis("BotVertical"));

        input = Vector2.ClampMagnitude(input, 1);
    }

    private void controlInput()
    {
        if (time_before_next_movement <= 0)
        {
            time_before_next_movement = max_time_until_next_movement;
            switch (current_action)
            {
                case action.WAITING: 
                    current_action = action.ROTATING;
                    break;
                case action.WALKING:
                    current_action = action.WAITING;
                    break;
                case action.ROTATING:
                    current_action = action.WALKING;
                    break;
            }
        }
        else
        {
            time_before_next_movement -= Time.deltaTime;
        }

        switch (current_action)
        {
            case action.WAITING: input = new Vector2(0, 0); break;
            case action.WALKING: input = new Vector2(0, 1); break;
            case action.ROTATING: transform.Rotate(0, 45 * Time.deltaTime, 0); break;
        }
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
}
