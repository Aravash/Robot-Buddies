using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public GameObject relatedBot;
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
        if (relatedBot.activeSelf)
        {
            renderer.material = mats[0];
        }
        else
        {
            renderer.material = mats[1];
        }
    }
}
