using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float force = 2f;
    public float halfHeight = .3f;
    private Rigidbody rb;
    private PlayerControlNoJump[] robots = new PlayerControlNoJump[4];
    int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
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

    //if grounded then spawn in the next unspawned robot
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
                var transform1 = transform;
                script.EnableBot(transform1.position, Quaternion.Euler(0, transform1.eulerAngles.y, 0));
                break;
            }
            Destroy(gameObject);
        }
    }
}
