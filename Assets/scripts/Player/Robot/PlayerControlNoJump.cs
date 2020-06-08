using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlNoJump : Player
{
    private Animator animator;
    private static readonly int Moving = Animator.StringToHash("moving");
    private bool activeBot = false;
    [SerializeField]
    private bool disabled = true; 

    void Start()
    {
        PlayerStart();
        animator = GetComponent<Animator>();
        speed = 2;
    }

    void Update()
    {
        if (disabled) return;

        StateSwitch();
        if (activeBot)
        {
            GetInput();

            Animate();

            CalculateCamera();

            CalculateGround();

            MovePlane();
            //currently robot does not move (via gravity) when out of robot controller
            //Need to fix this somehow without having robot deploy issue
            Gravity();
            
            jones.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }
       
    }
    
    /*
     * enable/disable bots when switching into/out of bot mode
     */
    private void StateSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            activeBot = !activeBot;
            animator.SetBool(Moving, false);
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

    public bool IsDisabled()
    {
        return disabled;
    }

    public void DisableBot()
    {
        activeBot = false;
        animator.SetBool(Moving, false);
        disabled = true;
        transform.position = new Vector3(0, -10, 0);
        camera.gameObject.SetActive(false);
    }
    
    public void EnableBot(Vector3 pos, Quaternion rot)
    {
        var transform1 = transform;
        transform1.position = pos;
        transform1.rotation = rot;
        camera.gameObject.SetActive(true);
        disabled = false;
    }
}
