using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class Button : MonoBehaviour
{
    public Door powerTarget;
    public Renderer[] wireRenderer;
    public Material[] wireMat;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    private void OnTriggerEnter(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
        changeState(ref other, true, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
        changeState(ref other, false, 0);
    }

    private void changeState(ref Collider other, bool outcome, int matType)
    {
        if(other.CompareTag("robot") || other.CompareTag("Player"))
        {
            foreach (Renderer renderer in wireRenderer)
            {
                renderer.material = wireMat[matType];
            }

            powerTarget.SetOpen(outcome);
        }
    }
}
