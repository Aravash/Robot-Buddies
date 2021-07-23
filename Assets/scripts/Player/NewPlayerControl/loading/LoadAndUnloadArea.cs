using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LoadAndUnloadArea : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToLoad;

    [SerializeField] private GameObject ObjectToUnload;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(ObjectToLoad, Vector3.zero, Quaternion.identity, null);
            Destroy(ObjectToUnload);//need to find a way to assign ObjectToUnload on later doors
        }
    }
}
