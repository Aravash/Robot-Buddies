using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCodec : MonoBehaviour
{
    public CameraLook cam;
    public GameObject codecObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAltControl>().hasCodec = true;
            cam.hasCodec = true;
            codecObj.SetActive(true);
            Destroy(gameObject);
        }
    }
}
