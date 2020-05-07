using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public GameObject relatedBot;
    public float lightIntensity = 2;
    private new Light light;
    
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (relatedBot.activeSelf)
        {
            light.intensity = lightIntensity;
        }
        else
        {
            light.intensity = 0;
        }
    }
}
