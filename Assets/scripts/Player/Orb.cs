using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float halfHeight = .3f;
    private Rigidbody rb;
    private PlayerControlNoJump[] robots = new PlayerControlNoJump[4];
    int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        layerMask = ~layerMask;
    }

    public void SetBots(ref PlayerControlNoJump[] bots)
    {
        for (int i = 0; i < 4; i++)
        {
            robots[i] = bots[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, halfHeight))
        {
            Debug.DrawRay(transform.position+Vector3.up*0.1f, -Vector3.up *hit.distance, Color.yellow, layerMask);
            foreach (PlayerControlNoJump script in robots)
            {
                if (!script.IsDisabled())
                {
                    continue;
                }
                //ideally its the Y rot of the orb and not quat.identity
                script.EnableBot(transform.position, Quaternion.identity);
                break;
            }
            Destroy(gameObject);
        }
    }
}
