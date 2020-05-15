using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform closedPos;
    public Transform openPos;

    // Movement speed in units per second.
    public float speed = 1.0F;

    private static bool isOpen = false; //default: false

    public static void SetOpen(bool state)
    {
        isOpen = state;
    }

    // Move to the target end position.
    void Update()
    {
        if (isOpen)
        {
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(transform.position, openPos.position, speed * Time.deltaTime);
        }
        else
        {
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(transform.position, closedPos.position, speed);
        }
    }
}
