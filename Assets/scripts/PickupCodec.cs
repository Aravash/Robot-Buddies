using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCodec : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.has_codec = true;
            Destroy(gameObject);
        }
    }
}
