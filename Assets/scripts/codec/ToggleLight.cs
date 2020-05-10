using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public PlayerControlNoJump relatedBot;
    public Material[] mats;
    private new Renderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (relatedBot.IsDisabled())
        {
            renderer.material = mats[1];
        }
        else
        {
            renderer.material = mats[0];
        }
    }
}
